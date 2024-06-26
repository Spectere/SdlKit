using System.Runtime.InteropServices;

namespace Spectere.SdlKit;

/// <summary>
/// Represents a color in the format expected by SdlKit.
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

    /// <summary>
    /// Initializes a new <see cref="SdlColor"/> with the default values.
    /// </summary>
    public SdlColor() { }

    /// <summary>
    /// Initializes a new <see cref="SdlColor"/> with defined color components.
    /// </summary>
    /// <param name="red">The red component of the new <see cref="SdlColor"/>. This can be any valid <see cref="byte"/>
    /// value.</param>
    /// <param name="green">The green component of the new <see cref="SdlColor"/>. This can be any valid
    /// <see cref="byte"/> value.</param>
    /// <param name="blue">The blue component of the new <see cref="SdlColor"/>. This can be any valid
    /// <see cref="byte"/> value.</param>
    /// <param name="alpha">The alpha component of the new <see cref="SdlColor"/>. This can be any valid
    /// <see cref="byte"/> value. If this is not specified, this defaults to <see cref="byte.MaxValue"/>.</param>
    public SdlColor(byte red, byte green, byte blue, byte alpha = byte.MaxValue) {
        R = red;
        G = green;
        B = blue;
        A = alpha;
    }

    public static bool operator ==(SdlColor left, SdlColor right) =>
        left.R == right.R
        && left.G == right.G
        && left.B == right.B
        && left.A == right.A;

    public static bool operator !=(SdlColor left, SdlColor right) => !(left == right);
    
    /// <summary>
    /// Compares two <see cref="SdlColor"/> structures for equality.
    /// </summary>
    /// <param name="other">The <see cref="SdlColor"/> that should be compared to this instance.</param>
    /// <returns><c>true</c> if the structures are equal, otherwise <c>false</c>.</returns>
    public bool Equals(SdlColor other) => this == other;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Padding other && Equals(other);
    
    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(R, G, B, A);
}
