using System;

namespace EffectSystem.Core.Domain.Events;

public sealed class EffectContext
{
    public float Magnitude { get; }
    public int Stacks { get; }

    public EffectContext(float magnitude, int stacks = 1)
    {
        if (stacks <= 0)
            throw new ArgumentOutOfRangeException(nameof(stacks));

        Magnitude = magnitude;
        Stacks = stacks;
    }
}