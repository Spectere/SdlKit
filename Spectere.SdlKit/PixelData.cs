using Spectere.SdlKit.Renderables;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit;

/// <summary>
/// The pixel data for an <see cref="Image"/>.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct PixelData {
    /// <summary>
    /// Pixel data, represented as an array of bytes. This is expected to be in the ABGR8888 pixel format on
    /// little-endian systems and RGBA8888 on big-endian systems.
    /// </summary>
    [FieldOffset(0)]
    public byte[] ByByte;
    
    /// <summary>
    /// Pixel data, represented as an array of <see cref="SdlColor"/> structs.
    /// </summary>
    [FieldOffset(0)]
    public SdlColor[] ByColor;

    /// <summary>
    /// Pixel data, represented as an array of unsigned integers. This is expected to be in the ABGR8888 pixel format
    /// on little-endian systems and RGBA8888 on big-endian systems.
    /// </summary>
    [FieldOffset(0)]
    public uint[] ByUint;

    /// <summary>
    /// Initializes this <see cref="PixelData"/> and its child arrays.
    /// </summary>
    /// <param name="size">The number of pixels that should be represented by this structure.</param>
    public PixelData(int size) {
        ByUint = new uint[size];

        // This section of memory was already initialized by the above statement, so there's no need to
        ByByte = GC.AllocateUninitializedArray<byte>(size * 4);
        ByColor = GC.AllocateUninitializedArray<SdlColor>(size);
    }
}
