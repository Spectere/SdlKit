using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Kernel32;

/// <summary>
/// Functions defined in <c>profileapi.h</c>.
/// </summary>
internal class ProfileApi {
    /// <summary>
    /// Retrieves the current value of the performance counter, which is a high resolution (&lt;1µs) time stamp that
    /// can be used for time-interval measurements.
    /// </summary>
    /// <param name="lpPerformanceCount">A pointer to a variable that receives the current performance-counter value,
    /// in counts.</param>
    /// <returns><c>true</c> if the function call was successful, otherwise <c>false</c>.</returns>
    [DllImport(Lib.NtKernel, SetLastError = true)]
    internal static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

    /// <summary>
    /// Retrieves the frequency of the performance counter. The frequency of the performance counter is fixed at system
    /// boot and is consistent across all processors. Therefore, the frequency need only be queried upon application
    /// initialization, and the result can be cached.
    /// </summary>
    /// <param name="lpFrequency">A pointer to a variable that receives the current performance-counter frequency, in
    /// counts per second.</param>
    /// <returns>This will always return <c>true</c> on Windows systems that support modern versions of .NET.</returns>
    [DllImport(Lib.NtKernel, SetLastError = true)]
    internal static extern bool QueryPerformanceFrequency(out long lpFrequency);
}
