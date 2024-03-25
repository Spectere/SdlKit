using Spectere.SdlKit.Interop.LibC.Support.Time;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.LibC;

/// <summary>
/// Contains interop functions contained in <c>time.h</c>.
/// </summary>
internal static class Time {
    /// <summary>
    /// Suspends thread execution for a measured interval.
    /// </summary>
    /// <param name="requested">The requested sleep time.</param>
    /// <param name="remaining">The amount of time that was not slept (<paramref name="requested"/> minus the actual
    /// time slept).</param>
    /// <returns>This function returns 0 if successful and -1 if an error has occurred. If an error has occurred it
    /// can be read using <see cref="Marshal.GetLastPInvokeError"/>.</returns>
    [DllImport(Lib.LibC, EntryPoint = "nanosleep", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
    internal static extern int Nanosleep(in TimeSpec requested, out TimeSpec remaining);
}
