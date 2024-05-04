namespace Spectere.SdlKit.Interop.Sdl.Support.Audio;

/// <summary>
/// Extension methods for the <see cref="SdlAudioFormat"/> enumeration.
/// </summary>
internal static class SdlAudioFormatExtension {
    /// <summary>
    /// Gets the number of bits per sample in this <see cref="SdlAudioFormat"/>.
    /// </summary>
    /// <param name="format">The <see cref="SdlAudioFormat"/> enumeration to evaluate.</param>
    /// <returns>The number of bits per sample in this <see cref="SdlAudioFormat"/>.</returns>
    internal static int GetBitSize(this SdlAudioFormat format) => (int)format & Sdl.Audio.BitSizeMask;

    /// <summary>
    /// Gets the endianness in this <see cref="SdlAudioFormat"/>.
    /// </summary>
    /// <param name="format">The <see cref="SdlAudioFormat"/> enumeration to evaluate.</param>
    /// <returns>The endianness described of this <see cref="SdlAudioFormat"/>.</returns>
    internal static Endian GetEndianness(this SdlAudioFormat format) => ((int)format & Sdl.Audio.BigEndianMask) != 0
        ? Endian.BigEndian
        : Endian.LittleEndian;

    /// <summary>
    /// Gets whether or not this <see cref="SdlAudioFormat"/> represents a floating point sample type.
    /// </summary>
    /// <param name="format">The <see cref="SdlAudioFormat"/> enumeration to evaluate.</param>
    /// <returns><c>true</c> if this <see cref="SdlAudioFormat"/> describes a floating-point sample format.</returns>
    internal static bool GetIsFloat(this SdlAudioFormat format) => ((int)format & Sdl.Audio.FloatMask) != 0;
    
    /// <summary>
    /// Gets whether or not this <see cref="SdlAudioFormat"/> represents a signed sample type.
    /// </summary>
    /// <param name="format">The <see cref="SdlAudioFormat"/> enumeration to evaluate.</param>
    /// <returns><c>true</c> if this <see cref="SdlAudioFormat"/> describes a signed sample format.</returns>
    internal static bool GetIsSigned(this SdlAudioFormat format) => ((int)format & Sdl.Audio.SignedMask) != 0;
}
