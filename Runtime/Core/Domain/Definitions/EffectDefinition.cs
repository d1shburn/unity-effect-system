using System;

namespace EffectSystem.Core.Domain.Definitions;

public sealed class EffectDefinition
{
    public float? Duration { get; }
    public float? TickInterval { get; }
    public int MaxStacks { get; }

    public EffectDefinition(float? duration, float? tickInterval,
        int maxStacks = 0)
    {
        if (duration is <= 0f)
            throw new ArgumentOutOfRangeException(nameof(duration));

        if (tickInterval is <= 0f)
            throw new ArgumentOutOfRangeException(nameof(tickInterval));

        if (maxStacks < 0)
            throw new ArgumentOutOfRangeException(nameof(maxStacks));

        Duration = duration;
        TickInterval = tickInterval;
        MaxStacks = maxStacks;
    }
}