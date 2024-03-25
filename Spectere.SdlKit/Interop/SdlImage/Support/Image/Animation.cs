using Spectere.SdlKit.Interop.Sdl.Support.Surface;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.SdlImage.Support.Image;

/// <summary>
/// Represents a loaded animation.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct Animation {
    /// <summary>The width of the animation.</summary>
    internal int Width;
    
    /// <summary>The height of the animation.</summary>
    internal int Height;
    
    /// <summary>The number of frames in the animation.</summary>
    internal int Count;
    
    /// <summary>An array of SDL <see cref="SdlSurface"/> objects containing the animation frames.</summary>
    internal unsafe SdlSurface* Frames;
    
    /// <summary>The delay between each of the frames.</summary>
    internal unsafe int* Delays;
}
