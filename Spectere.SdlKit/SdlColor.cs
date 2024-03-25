using System.Runtime.InteropServices;

namespace Spectere.SdlKit;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// The bits of this structure can be directly reinterpreted as an integer-packed color which uses the RGBA32
/// format (ABGR8888 on little-endian systems and RGBA8888 on big-endian systems).
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct SdlColor {
    /// <summary>
    /// The red component of this color. This can range from 0-255.
    /// </summary>
    public byte R;

    /// <summary>
    /// The green component of this color. This can range from 0-255.
    /// </summary>
    public byte G;

    /// <summary>
    /// The blue component of this color. This can range from 0-255.
    /// </summary>
    public byte B;

    /// <summary>
    /// The alpha component of this color. This can range from 0-255.
    /// </summary>
    public byte A;
    
    /// <summary>
    /// Returns the value of this <see cref="SdlColor"/> in hex format (#RRGGBBAA).
    /// </summary>
    /// <returns>The value of this <see cref="SdlColor"/> in hex format (#RRGGBBAA).</returns>
    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}{A:X2}";
}
