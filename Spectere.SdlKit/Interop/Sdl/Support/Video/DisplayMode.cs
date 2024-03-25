using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Video;

/// <summary>
/// The structure that defines a display mode.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct DisplayMode {
    /// <summary>
    /// The pixel format of the display mode.
    /// </summary>
    internal uint Format;
    
    /// <summary>
    /// The width of the display mode, in screen coordinates.
    /// </summary>
    internal int Width;
    
    /// <summary>
    /// The height of the display mode, in screen coordinates.
    /// </summary>
    internal int Height;
    
    /// <summary>
    /// The refresh rate (or zero for unspecified).
    /// </summary>
    internal int RefreshRate;
    
    /// <summary>
    /// Driver-specific data. Initialize to 0.
    /// </summary>
    internal IntPtr DriverData;  // void*
}
