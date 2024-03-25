namespace Spectere.SdlKit;

/// <summary>
/// Defines a timer definition. This is used by <see cref="Timer"/> implementations to track and store data about
/// registered timers.
/// </summary>
internal class TimerDefinition {
    /// <summary>
    /// The unique ID identifying this timer.
    /// </summary>
    public Guid TimerId = Guid.NewGuid();
    
    /// <summary>
    /// The callback method that the <see cref="Timer"/> class will call when this timer is fired.
    /// </summary>
    public Timer.TimerCallback Callback;

    /// <summary>
    /// How often this timer is scheduled to be executed, in nanoseconds.
    /// </summary>
    private readonly long _period;
    
    /// <summary>
    /// The last time this timer was executed, in nanoseconds.
    /// </summary>
    private long _lastExecuted;
    
    /// <summary>
    /// The next time this timer should be executed, in nanoseconds.
    /// </summary>
    internal long NextExecution;

    /// <summary>
    /// If this is set to <c>false</c>, this timer will always fire the expected number of times regardless of how
    /// far it falls behind. This should remain set to <c>true</c> whenever possible.
    /// </summary>
    private readonly bool _allowSkip;

    /// <summary>
    /// Defines a new timer.
    /// </summary>
    /// <param name="callback">The callback method that the <see cref="Timer"/> class will call when this timer is
    /// fired.</param>
    /// <param name="period">How often this timer is scheduled to be executed, in nanoseconds.</param>
    /// <param name="allowSkip">If this is set to <c>false</c>, this timer will always fire the expected number of
    /// times regardless of how far it falls behind. This should remain set to <c>true</c> whenever possible.</param>
    public TimerDefinition(Timer.TimerCallback callback, long period, bool allowSkip = true) {
        Callback = callback;
        _period = period;
        _allowSkip = allowSkip;
    }

    /// <summary>
    /// Resets the execution times for this timer. This should be done prior to starting a timer loop.
    /// </summary>
    public void Reset() {
        _lastExecuted = NextExecution = 0;
    }

    /// <summary>
    /// Updates the timestamps for this <see cref="TimerDefinition"/> and returns the delta time.
    /// </summary>
    /// <param name="currentTime">The amount of time since this timer was fired, in nanoseconds.</param>
    /// <returns></returns>
    public long Update(long currentTime) {
        var delta = currentTime - _lastExecuted;
        NextExecution += _period;
        _lastExecuted = currentTime;

        if(!_allowSkip && NextExecution < currentTime) {
            NextExecution = currentTime;
        }

        return delta;
    }
}
