using Spectere.SdlKit.Exceptions;
using PosixErrorCode = Spectere.SdlKit.Interop.LibC.Support.ErrNo.ErrorCode;
using PosixTime = Spectere.SdlKit.Interop.LibC.Time;
using PosixTimeSupport = Spectere.SdlKit.Interop.LibC.Support.Time;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Win32Time = Spectere.SdlKit.Interop.Kernel32.ProfileApi;

namespace Spectere.SdlKit;

/// <summary>
/// Provides a periodic timer when inherited. This timer should be roughly as accurate as the one provided by the host
/// operating system.
/// </summary>
public class Timer {
    /// <summary>
    /// A convenient constant to help convert nanoseconds into seconds where necessary.
    /// </summary>
    private const long NanosecondsInSecond = 1000000000;
    
    /// <summary>
    /// The time the next timer will need to be serviced, in nanoseconds.
    /// </summary>
    private long _nextTimer;

    /// <summary>
    /// The frequency of the system timer in ticks per second.
    /// </summary>
    private readonly long _timerFrequency;
    
    /// <summary>
    /// A list containing the registered timers.
    /// </summary>
    private readonly List<TimerDefinition> _timers = new();
    
    /// <summary>
    /// If this is <c>true</c>, the timer is currently running. If this is set to <c>false</c>, the timer loop will
    /// exit at the conclusion of the current loop.
    /// </summary>
    public bool TimerRunning { get; set; }
    
    /// <summary>
    /// Defines a timer callback method signature. This pattern should be called when registering a timer with this
    /// handler.
    /// </summary>
    /// <param name="delta">The amount of time that passed since the last time this was called, in nanoseconds.</param>
    public delegate void TimerCallback(long delta);

    /// <summary>
    /// The timer frequency on Win32-based platforms. This is only used if the <see cref="Nanosleep"/> method is called
    /// on Windows-based systems.
    /// </summary>
    private readonly long _win32TimerFrequency;

    /// <summary>
    /// Initializes a <see cref="Timer"/>.
    /// </summary>
    public Timer() {
        _timerFrequency = Stopwatch.Frequency;

        if(OperatingSystem.IsWindows()) {
            _ = Win32Time.QueryPerformanceFrequency(out _win32TimerFrequency);
        }
    }

    /// <summary>
    /// Registers a timer with this handler.
    /// </summary>
    /// <param name="callback">The method that should be called when this timer is fired.</param>
    /// <param name="period">The frequency that this timer should be called, in nanoseconds.</param>
    /// <returns>A <see cref="Guid"/> that uniquely identifies this timer.</returns>
    public Guid AddTimer(TimerCallback callback, long period) {
        var newTimer = new TimerDefinition(callback, period);
        _timers.Add(newTimer);
        return newTimer.TimerId;
    }

    /// <summary>
    /// Unregisters a timer from this handler.
    /// </summary>
    /// <param name="timerId">A <see cref="Guid"/> that identifies the timer to remove.</param>
    public void DeleteTimer(Guid timerId) {
        var timer = _timers.SingleOrDefault(x => x.TimerId == timerId);
        if(timer is null) {
            return;
        }

        _timers.Remove(timer);
    }

    /// <summary>
    /// Used to convert hertz (times per second) into nanoseconds.
    /// </summary>
    /// <param name="hertz">A number of times per second.</param>
    /// <returns>The number of nanoseconds that correspond to the value in <paramref name="hertz"/>.</returns>
    public static long HertzToNanoseconds(long hertz) => TicksToNanoseconds(1, hertz);

    /// <summary>
    /// Converts nanoseconds to timer ticks, based on a particularly frequency.
    /// </summary>
    /// <param name="nanoseconds">The number of nanoseconds.</param>
    /// <param name="frequency">The timer frequency (number of ticks per second).</param>
    /// <returns>The number of ticks.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long NanosecondsToTicks(long nanoseconds, long frequency) =>
        (long)((double)nanoseconds / NanosecondsInSecond * frequency);

    /// <summary>
    /// Suspends the thread for a period of time, in nanoseconds.
    /// </summary>
    /// <param name="nanoseconds">The amount of time to sleep, in nanoseconds.</param>
    private void Nanosleep(long nanoseconds) {
        if(OperatingSystem.IsWindows()) {
            // Windows is basically the odd man out these days (barring Nintendo consoles). Pretty much everything else
            // is POSIX-y.
            
            // Staying asleep for the entire duration is not guaranteed, so we grab the starting time, then
            // call QueryPerformanceCounter until the entire duration has passed.
            Win32Time.QueryPerformanceCounter(out var start);
            var ticks = NanosecondsToTicks(nanoseconds, _win32TimerFrequency);
            
            while(true) {
                Win32Time.QueryPerformanceCounter(out var now);

                if(now - start >= ticks) {
                    break;
                }
            }
        } else {
            // POSIX implementation. This should cover the rest of the platforms supported by .NET.
            
            // As with Win32, there's no guarantee that nanosleep will continue uninterrupted. If it returns 0,
            // we can move on. If it returns -1, we'll need to check errno to see what went wrong and deal with
            // the issue. Odds are we can just run it again with the remaining time.
            var request = new PosixTimeSupport.TimeSpec {
                Seconds = nanoseconds / NanosecondsInSecond,
                Nanoseconds = nanoseconds % NanosecondsInSecond
            };

            while(PosixTime.Nanosleep(in request, out var remaining) != 0) {
                var error = Marshal.GetLastPInvokeError();

                // Check for unrecoverable errors or errors caused by malformed input data.
                switch(error) {
                    case PosixErrorCode.EINVAL:
                        // A nanosecond value of under 0 or over NanosecondsInSecond was requested. This indicates an
                        // error in this library. Throw an exception.
                        throw new SdlKitException($"nanosleep() returned EINVAL. tv_sec is {request.Seconds}; tv_nsec is {request.Nanoseconds}");
                    case PosixErrorCode.EFAULT:
                        // [BSD/macOS] Either rqtp (request) or rmtp (remaining) points to memory that is not a valid
                        // part of the process address space. This really shouldn't happen with our managed structs,
                        // but throw an exception if it does.
                        throw new SdlKitException("nanosleep() returned EFAULT.");
                    case PosixErrorCode.ENOSYS:
                        // [BSD/macOS] nanosleep() is not supported by this implementation. It's unlikely that this will
                        // ever be returned, but we'll "handle" it regardless.
                        throw new NotSupportedException("nanosleep() is not supported by this platform!");
                }

                // If we got here, the error should be EINTR (unless something went horribly wrong). Regardless, run
                // the loop again using the remaining time.
                request = remaining;
            }
        }
    }

    /// <summary>
    /// Resets all timers. This must be run prior to all timer loops.
    /// </summary>
    private void ResetTimers() {
        foreach(var timer in _timers) {
            timer.Reset();
        }
    }

    /// <summary>
    /// Starts a timer loop. This can only be
    /// </summary>
    public void StartTimerLoop() {
        var stopwatch = new Stopwatch();
        TimerRunning = true;
        ResetTimers();

        stopwatch.Start();
        while(TimerRunning) {
            var currentTime = TicksToNanoseconds(stopwatch.ElapsedTicks, _timerFrequency);
            _nextTimer = long.MaxValue;
            
            foreach(var timer in _timers) {
                if(timer.NextExecution <= currentTime) {
                    var delta = timer.Update(currentTime);
                    timer.Callback(delta);

                    if(_nextTimer > timer.NextExecution) {
                        _nextTimer = timer.NextExecution;
                    }
                }

                currentTime = TicksToNanoseconds(stopwatch.ElapsedTicks, _timerFrequency);
            }

            if(_nextTimer == long.MaxValue) {
                // Huh?
                Console.Write("Timer: unable to calculate wait time ");
                
                if(_timers.Count == 0) {
                    // Hopefully this is why.
                    Console.WriteLine("(all timers have been removed!)");
                } else {
                    // If not, it's some sort of SDLKit issue (or freak accident).
                    Console.WriteLine("(unable to determine the root cause)");
                }
            }

            // Wait until the next timer should fire.
            if(_nextTimer > currentTime) {
                Nanosleep(_nextTimer - currentTime);
            }
        }
        stopwatch.Stop();
    }
    
    /// <summary>
    /// Converts timer ticks to nanoseconds, based on a particularly frequency.
    /// </summary>
    /// <param name="ticks">The number of ticks.</param>
    /// <param name="frequency">The timer frequency (number of ticks per second).</param>
    /// <returns>The number of ticks.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long TicksToNanoseconds(long ticks, long frequency) =>
        (long)((double)NanosecondsInSecond / frequency * ticks);
}
