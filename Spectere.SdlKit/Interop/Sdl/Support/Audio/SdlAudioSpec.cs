using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Audio; 

/// <summary>
/// A structure that contains the audio output format. It also contains a
/// callback that is called when the audio device needs more data.
/// </summary>
internal struct SdlAudioSpec {
    #pragma warning disable 0649

    /// <summary>The DSP frequency, in hertz.</summary>
    internal int Freq;

    /// <summary>The audio data format.</summary>
    internal SdlAudioFormat Format;

    /// <summary>The number of sound channels.</summary>
    internal byte Channels;

    /// <summary>Audio buffer silence value (calculated).</summary>
    internal byte Silence;

    /// <summary>Audio buffer size in samples (power of 2).</summary>
    internal ushort Samples;

    /// <summary>Unused.</summary>
    internal ushort Padding;
    
    /// <summary>Audio buffer size in bytes (calculated).</summary>
    internal uint Size;

    /// <summary>The function to call when the audio device needs more data.</summary>
    [MarshalAs(UnmanagedType.FunctionPtr)]
    internal Sdl.Audio.SdlAudioCallback Callback;
            
    /// <summary>A pointer that is passed to the <see cref="Callback"/> function.</summary>
    internal IntPtr Userdata;
    
    #pragma warning restore 0649
}
