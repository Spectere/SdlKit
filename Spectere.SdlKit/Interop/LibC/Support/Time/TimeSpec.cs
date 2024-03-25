using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.LibC.Support.Time;

/// <summary>
/// A .NET representation of the <c>timespec</c> structure in <c>time.h</c>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct TimeSpec {
    /// <summary>
    /// A number of seconds.
    /// </summary>
    internal long Seconds;
    
    /// <summary>
    /// A number of nanoseconds. This field must be in the range of 0 to 999999999.
    /// </summary>
    internal long Nanoseconds;
}
