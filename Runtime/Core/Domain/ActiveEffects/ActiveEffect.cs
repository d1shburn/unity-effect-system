using System;

using EffectSystem.Core.Domain.Definitions;
using EffectSystem.Core.Domain.Timing;

namespace EffectSystem.Core.Domain.ActiveEffects;

public sealed class ActiveEffect
{
    private readonly CountdownTimer? _durationTimer;
    private readonly CountdownTimer? _tickTimer;

    public EffectDefinition Definition { get; }
    public int Stacks { get; private set; }
    public bool IsExpired { get; private set; }

    public ActiveEffect(EffectDefinition definition)
    {
        if (definition == null)
            throw new ArgumentNullException(nameof(definition));

        _durationTimer = CreateTimer(definition.Duration);
        _tickTimer = CreateTimer(definition.TickInterval);

        Definition = definition;
        Stacks = 1;
    }

    private static CountdownTimer? CreateTimer(float? duration)
    {
        if (duration is not { } value)
            return null;

        var timer = new CountdownTimer(value);
        timer.Start();

        return timer;
    }

    public EffectUpdateResult Update(float deltaTime)
    {
        if (deltaTime < 0f ||
            float.IsNaN(deltaTime) ||
            float.IsInfinity(deltaTime))
        {
            throw new ArgumentOutOfRangeException(nameof(deltaTime));
        }

        if (IsExpired)
            return new EffectUpdateResult(0, true);

        if (deltaTime == 0f)
            return new EffectUpdateResult(0, false);

        var tickCount = 0;
        var remainingDelta = deltaTime;

        while (remainingDelta > 0f)
        {
            var step = remainingDelta;

            if (_durationTimer is not null)
                step = Math.Min(step, _durationTimer.RemainingTime);

            if (_tickTimer is not null)
                step = Math.Min(step, _tickTimer.RemainingTime);

            if (step <= 0f)
                break;

            var tickOccurred = _tickTimer?.Update(step) ?? false;
            var effectExpired = _durationTimer?.Update(step) ?? false;

            remainingDelta -= step;

            if (tickOccurred && _tickTimer is not null)
            {
                tickCount++;
                _tickTimer.Reset();
                _tickTimer.Start();
            }

            if (effectExpired)
            {
                IsExpired = true;
                _tickTimer?.Stop();
                break;
            }
        }

        return new EffectUpdateResult(tickCount, IsExpired);
    }

    public bool TryAddStack()
    {
        if (IsExpired || Stacks >= Definition.MaxStacks)
            return false;

        Stacks++;
        return true;
    }
}