using System;

namespace EffectSystem.Core.Domain.Timing;

public sealed class CountdownTimer
{
    private readonly float _initialDuration;

    public float RemainingTime { get; private set; }
    public bool IsRunning { get; private set; }

    public CountdownTimer(float initialDuration)
    {
        if (initialDuration < 0f ||
            float.IsNaN(initialDuration) ||
            float.IsInfinity(initialDuration))
        {
            throw new ArgumentOutOfRangeException(nameof(initialDuration));
        }

        _initialDuration = initialDuration;

        RemainingTime = initialDuration;
    }

    public void Start()
    {
        if (RemainingTime <= 0f)
        {
            IsRunning = false;
            return;
        }

        IsRunning = true;
    }

    public void Reset()
    {
        RemainingTime = _initialDuration;
        IsRunning = false;
    }

    public void Stop() =>
        IsRunning = false;

    public bool Update(float deltaTime)
    {
        if (deltaTime < 0f ||
            float.IsNaN(deltaTime) ||
            float.IsInfinity(deltaTime))
        {
            throw new ArgumentOutOfRangeException(nameof(deltaTime));
        }

        if (IsRunning == false)
            return false;

        RemainingTime -= deltaTime;

        if (RemainingTime > 0f)
            return false;

        RemainingTime = 0f;
        IsRunning = false;
        return true;
    }
}