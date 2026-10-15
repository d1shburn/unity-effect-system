using System;
using EffectSystem.Core.Domain.Definitions;

namespace EffectSystem.Core.Domain.ActiveEffects;

public sealed class ActiveEffect
{
    private const int InitialStacks = 1;

    public EffectDefinition Definition { get; }
    public int Stacks { get; private set; }
    public float? RemainingDuration { get; private set; }
    public float? TimeUntilNextTick { get; private set; }

    public ActiveEffect(EffectDefinition definition)
    {
        if (definition == null)
            throw new ArgumentNullException(nameof(definition));

        Definition = definition;
        Stacks = InitialStacks;
        RemainingDuration = definition.Duration;
        TimeUntilNextTick = definition.TickInterval;
    }
}