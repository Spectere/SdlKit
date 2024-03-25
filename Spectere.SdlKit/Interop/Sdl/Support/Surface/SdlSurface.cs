using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Surface;

/// <summary>
/// A collection of pixels used in software blitting.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct SdlSurface {
    private readonly IntPtr _sdlSurface;

    /// <summary>
    /// If <c>true</c>, this <see cref="SdlSurface"/> is <c>null</c> and should not be used.
    /// </summary>
    public bool IsNull => _sdlSurface.Equals(IntPtr.Zero);
    

    // This probably isn't the *best* way to do this, but it's way too much fun (and SDL's ABI is quite stable, so we
    // should be able to get away with it). :)
    private static readonly int PointerWidth = IntPtr.Size;
    
    private static readonly int SdlSurfaceWidthOffset = 8 + PointerWidth;
    private static readonly int SdlSurfaceHeightOffset = SdlSurfaceWidthOffset + 4;
    private static readonly int SdlSurfacePitchOffset = SdlSurfaceHeightOffset + 4;
    private static readonly int SdlSurfacePixelsOffset = SdlSurfaceHeightOffset + 4;

    /// <summary>
    /// The width of the <see cref="SdlSurface"/>, or -1 if the surface is <c>null</c>
    /// </summary>
    public int Height => GetInteger(SdlSurfaceHeightOffset) ?? -1;

    /// <summary>
    /// The pitch (byte length of each row) of ths <see cref="SdlSurface"/>, or -1 if the surface is <c>null</c>.
    /// </summary>
    public int Pitch => GetInteger(SdlSurfacePitchOffset) ?? -1;
    
    /// <summary>
    /// An <see cref="IntPtr"/> pointing to the pixel array for this <see cref="SdlSurface"/>.
    /// </summary>
    public IntPtr Pixels => _sdlSurface + SdlSurfacePixelsOffset;

    /// <summary>
    /// The width of the <see cref="SdlSurface"/>, or -1 if the surface is <c>null</c>
    /// </summary>
    public int Width => GetInteger(SdlSurfaceWidthOffset) ?? -1;
    
    /// <summary>
    /// Gets an integer from the specified byte offset.
    /// </summary>
    /// <param name="byteOffset"></param>
    /// <returns>The requested integer, or <c>null</c> if the surface pointer is invalid.</returns>
    private int? GetInteger(int byteOffset) {
        if(IsNull) {
            return null;
        }
        
        return Marshal.ReadInt32(_sdlSurface + byteOffset);
    }
}
