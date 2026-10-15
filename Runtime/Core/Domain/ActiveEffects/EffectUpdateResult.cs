using System;

namespace EffectSystem.Core.Domain.ActiveEffects;

public readonly struct EffectUpdateResult
{
    public int TickCount { get; }
    public bool IsExpired { get; }

    public EffectUpdateResult(int tickCount, bool isExpired)
    {
        if (tickCount < 0)
            throw new ArgumentOutOfRangeException(nameof(tickCount));

        TickCount = tickCount;
        IsExpired = isExpired;
    }
}