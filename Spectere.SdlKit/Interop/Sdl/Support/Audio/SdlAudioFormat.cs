namespace Spectere.SdlKit.Interop.Sdl.Support.Audio; 

/// <summary>
/// A list of audio formats supported by SDL.
/// </summary>
internal enum SdlAudioFormat : ushort {
    /// <summary>Unsigned byte. Equivalent to AUDIO_U8.</summary>
    UnsignedByte = 0x0008,

    /// <summary>Signed byte. Equivalent to AUDIO_S8.</summary>
    SignedByte = 0x8008,

    /// <summary>Little-endian unsigned short. Equivalent to AUDIO_U16LSB.</summary>
    UnsignedShortLittleEndian = 0x0010,

    /// <summary>Little-endian signed short. Equivalent to AUDIO_S16LSB.</summary>
    SignedShortLittleEndian = 0x8010,

    /// <summary>Big-endian unsigned short. Equivalent to AUDIO_U16MSB.</summary>
    UnsignedShortBigEndian = 0x1010,

    /// <summary>Big-endian signed short. Equivalent to AUDIO_S16MSB.</summary>
    SignedShortBigEndian = 0x9010,

    /// <summary>Little-endian signed int. Equivalent to AUDIO_S32LSB.</summary>
    SignedIntLittleEndian = 0x8020,

    /// <summary>Big-endian signed int. Equivalent to AUDIO_S32MSB.</summary>
    SignedIntBigEndian = 0x9020,

    /// <summary>Little-endian floats. Equivalent to AUDIO_F32LSB.</summary>
    FloatLittleEndian = 0x8120,

    /// <summary>Big-endian floats. Equivalent to AUDIO_F32MSB.</summary>
    FloatBigEndian = 0x9120
}
