using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Pixels;

/// <summary>
/// A defined SDL pixel format.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct PixelFormat {
    internal readonly uint Format;
    internal readonly unsafe Palette* Palette;
    internal readonly byte BitsPerPixel;
    internal readonly byte BytesPerPixel;
    internal unsafe fixed byte Padding[2];
    internal readonly uint RedMask;
    internal readonly uint GreenMask;
    internal readonly uint BlueMask;
    internal readonly uint AlphaMask;
    internal readonly byte RedLoss;
    internal readonly byte GreenLoss;
    internal readonly byte BlueLoss;
    internal readonly byte AlphaLoss;
    internal readonly byte RedShift;
    internal readonly byte GreenShift;
    internal readonly byte BlueShift;
    internal readonly byte AlphaShift;
    internal readonly int RefCount;
    
    internal readonly unsafe PixelFormat* Next;
}
