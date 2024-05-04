using SdlAudio = Spectere.SdlKit.Interop.Sdl.Audio;
using Spectere.SdlKit.Exceptions;
using Spectere.SdlKit.Interop.Sdl.Support.Audio;

namespace Spectere.SdlKit;

/// <summary>
/// Represents an audio format. This can be used to both request an audio format from SDL, as well as return the
/// closest format that the target device supports.
/// </summary>
public record AudioSpec {
    /// <summary>
    /// Gets the number of bits that this <see cref="AudioSpec"/> is requesting or was assigned.
    /// </summary>
    public int BitSize { get; } = 32;

    /// <summary>
    /// Gets the buffer size, in bytes. This is calculated automatically based on the number of samples and audio format.
    /// </summary>
    public int BufferSize => Samples * BytesPerSample;

    /// <summary>
    /// Gets the number of bytes per sample.
    /// </summary>
    public int BytesPerSample => (BitSize / 8) * Channels;
    
    /// <summary>
    /// Gets the number of requested channels. Possible values are as follows:
    /// <list type="bullet">
    ///   <item>
    ///     <term>2</term>
    ///     <description>Stereo sound (left, right).</description>
    ///   </item>
    ///   <item>
    ///     <term>3</term>
    ///     <description>Stereo sound with separate subwoofer channel (left, right, LFE).</description>
    ///   </item>
    ///   <item>
    ///     <term>4</term>
    ///     <description>Quadraphonic sound (front-left, front-right, rear-left, rear-right).</description>
    ///   </item>
    ///   <item>
    ///     <term>5</term>
    ///     <description>4.1 sound (front-left, front-right, LFE, rear-left, rear-right).</description>
    ///   </item>
    ///   <item>
    ///     <term>6</term>
    ///     <description>5.1 sound (front-left, front-right, front-center, LFE, rear/side-left, rear/side-right).</description>
    ///   </item>
    ///   <item>
    ///     <term>7</term>
    ///     <description>6.1 sound (front-left, front-right, front-center, LFE, rear-center, rear/side-left, rear/side-right).</description>
    ///   </item>
    ///   <item>
    ///     <term>8</term>
    ///     <description>7.1 sound (front-left, front-right, front-center, LFE, rear-left, rear-right, side-left, side-right).</description>
    ///   </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// When sending audio data to SDLKit, the samples must be arranged in the order listed in the summary. For example,
    /// when sending stereo data, interleaved left and right values are expected. When sending quadraphonic audio data,
    /// data for each sample must be arranged as such: front-left, front-right, rear-left, rear-right.
    /// </remarks>
    public byte Channels { get; } = 2;

    /// <summary>
    /// Gets the size of the audio buffer, in samples.
    /// </summary>
    public ushort Samples { get; } = 1024;

    /// <summary>
    /// Gets the endianness of the requested or acquired audio stream.
    /// </summary>
    public Endian Endianness { get; } = Endian.LittleEndian;

    /// <summary>
    /// Gets the frequency of the requested audio stream, in hertz. Typical values for this field are 44100 and 48000.
    /// </summary>
    public int Frequency { get; } = 48000;

    /// <summary>
    /// Gets whether or not this <see cref="AudioSpec"/> is requesting or was assigned a floating point audio format.
    /// </summary>
    public bool IsFloat { get; }

    /// <summary>
    /// Gets whether or not this <see cref="AudioSpec"/> is requesting or was assigned a signed audio format.
    /// </summary>
    public bool IsSigned { get; } = true;

    /// <summary>
    /// Creates a new <see cref="AudioSpec"/>. If the values are not changed, this will specify a default specification
    /// of 48000hz stereo with 32-bit little-endian floating point samples and a buffer size of 1024 samples.
    /// </summary>
    public AudioSpec() {
        if(ValidateAudioFormat()) {
            return;
        }

        throw new SdlUnsupportedAudioFormat(BitSize, IsFloat, IsSigned);
    }

    /// <summary>
    /// Creates a new <see cref="AudioSpec"/>.
    /// </summary>
    /// <param name="frequency">The desired frequency, in hertz.</param>
    /// <param name="bitSize">The number of bits per sample.</param>
    /// <param name="isFloat"><c>true</c> if each sample is a floating-point value, or <c>false</c> if they are integers.</param>
    /// <param name="isSigned"><c>true</c> if each sample is a signed value, or <c>false</c> if they are unsigned. This
    /// value is ignored if <paramref name="isFloat"/> is set to <c>true</c>.</param>
    /// <param name="channels">The number of channels.</param>
    /// <param name="samples">The number of samples in the audio buffer.</param>
    /// <param name="endianness">The endianness of the sample format. This defaults to and should almost always be set
    /// to <see cref="Endian.LittleEndian"/>.</param>
    public AudioSpec(int frequency, int bitSize, bool isFloat, bool isSigned, byte channels, ushort samples, Endian endianness = Endian.LittleEndian)
        : this() {
        Frequency = frequency;
        BitSize = bitSize;
        IsFloat = isFloat;
        IsSigned = isSigned;
        Channels = channels;
        Samples = samples;
        Endianness = endianness;
    }

    /// <summary>
    /// Creates a new <see cref="AudioSpec"/>.
    /// </summary>
    /// <param name="frequency">The desired frequency, in hertz.</param>
    /// <param name="sdlFormat">A support SDL samples format.</param>
    /// <param name="channels">The number of channels.</param>
    /// <param name="samples">The number of samples in the audio buffer.</param>
    internal AudioSpec(int frequency, SdlAudioFormat sdlFormat, byte channels, ushort samples) : this(
        frequency: frequency,
        bitSize: sdlFormat.GetBitSize(),
        isFloat: sdlFormat.GetIsFloat(),
        isSigned: sdlFormat.GetIsSigned(),
        channels: channels,
        samples: samples,
        endianness: sdlFormat.GetEndianness()
    ) { }

    /// <summary>
    /// Creates a new <see cref="AudioSpec"/> based on an <see cref="SdlAudioSpec"/>.
    /// </summary>
    /// <param name="sdlAudioSpec">The <see cref="SdlAudioSpec"/> to use as a base for the new <see cref="AudioSpec"/>.
    /// Note that this will not transfer over the value of the <see cref="SdlAudioSpec.Callback"/> property.</param>
    internal AudioSpec(SdlAudioSpec sdlAudioSpec) : this(
        frequency: sdlAudioSpec.Freq,
        sdlFormat: sdlAudioSpec.Format,
        channels: sdlAudioSpec.Channels,
        samples: sdlAudioSpec.Samples
    ) { }

    /// <summary>
    /// Converts the SdlKit <see cref="AudioSpec"/> into an <c>SDL_AudioFormat</c> value.
    /// </summary>
    /// <returns>The <c>SDL_AudioFormat</c> equivalent of this SdlKit <see cref="AudioSpec"/>. Note that some values
    /// (such as <see cref="Frequency"/> and <see cref="Samples"/>) are not represented by <c>SDL_AudioFormat</c>.</returns>
    internal SdlAudioFormat GetSdlAudioFormat() => (SdlAudioFormat)(
        (IsSigned ? SdlAudio.SignedMask : 0)
      | (Endianness == Endian.BigEndian ? SdlAudio.BigEndianMask : 0)
      | (IsFloat ? SdlAudio.FloatMask : 0)
      | (BitSize & SdlAudio.BitSizeMask)
    );

    /// <summary>
    /// Converts this SdlKit <see cref="AudioSpec"/> into an SDL-compatible <see cref="SdlAudioSpec"/>. Note that some
    /// additional values (such as <see cref="SdlAudioSpec.Callback"/>) may still need to be set.
    /// </summary>
    /// <param name="audioSpec">The SdlKit <see cref="AudioSpec"/> to convert.</param>
    /// <returns>The <see cref="SdlAudioSpec"/> equivalent of the provided <see cref="AudioSpec"/>.</returns>
    internal static SdlAudioSpec ToSdlAudioSpec(AudioSpec audioSpec) => new() {
        Channels = audioSpec.Channels,
        Format = audioSpec.GetSdlAudioFormat(),
        Freq = audioSpec.Frequency,
        Samples = audioSpec.Samples
    };

    /// <summary>
    /// Converts this SdlKit <see cref="AudioSpec"/> into an SDL-compatible <see cref="SdlAudioSpec"/>. Note that some
    /// additional values (such as <see cref="SdlAudioSpec.Callback"/>) may still need to be set.
    /// </summary>
    /// <returns>The <see cref="SdlAudioSpec"/> equivalent of this <see cref="AudioSpec"/>.</returns>
    internal SdlAudioSpec ToSdlAudioSpec() => ToSdlAudioSpec(this);

    /// <summary>
    /// Validates whether or not the audio format (specifically the bit size, signed, and floating-point properties) for
    /// this <see cref="AudioSpec"/> are supported by SDL.
    /// </summary>
    /// <returns><c>true</c> if the format is supported by SDL, otherwise <c>false</c>.</returns>
    private bool ValidateAudioFormat() => BitSize switch {
        8 when !IsFloat => true,
        16 when !IsFloat => true,
        32 when IsSigned && !IsFloat => true,
        32 when IsFloat => true,
        _ => false
    };
}
