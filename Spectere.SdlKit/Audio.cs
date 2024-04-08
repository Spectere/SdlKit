using Spectere.SdlKit.Interop;
using Spectere.SdlKit.Interop.LibC;

namespace Spectere.SdlKit;

/// <summary>
/// Provides a means for an application to play audio via SDL.
/// </summary>
public class Audio {
    /// <summary>
    /// The audio buffer that the application should fill in for us.
    /// </summary>
    private float[] _buffer = Array.Empty<float>();
    
    /// <summary>
    /// A field that will point to the audio callback function in this class.
    /// </summary>
    private Interop.Sdl.Audio.SdlAudioCallback _callback;
    
    /// <summary>
    /// Raised when the audio subsystem is ready to receive more audio.
    /// </summary>
    public delegate void AudioRequestedEventHandler(object sender, int samplesRequested, ref float[] buffer);
    
    /// <summary>
    /// Raised when the audio subsystem is ready to receive more audio.
    /// </summary>
    public event AudioRequestedEventHandler? AudioRequested;

    /// <summary>
    /// If this is set to <c>true</c>, the audio buffer will be initialized to 0 when passed to the application. If
    /// this is <c>false</c>, the buffer will not be initialized and may contain stale data. This defaults to
    /// <c>false</c>.
    /// </summary>
    public bool ClearBufferOnCallback { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Audio() {
        LibraryResolver.SetSdlKitLibraryResolver();  // Required for memset.
        
        _callback = SdlCallback;
    }

    /// <summary>
    /// The SDL audio callback function.
    /// </summary>
    /// <param name="userdata">Custom user data. This is not used by SdlKit.</param>
    /// <param name="stream">A pointer to the audio stream that should be filled by the application.</param>
    /// <param name="len">The amount of data to fill the stream with, in bytes.</param>
    private unsafe void SdlCallback(IntPtr userdata, IntPtr stream, int len) {
        if(AudioRequested is null) {
            // No subscriber. Fill the stream with silence.
            CMemory.Memset(stream.ToPointer(), 0, (nuint)len);
            return;
        }

        var samples = 0;  // TODO: Get the number of samples that need filled. Resize the buffer array if necessary.
        AudioRequested(this, samples, ref _buffer);
        
        // TODO: Convert the audio stream from float to the desired format, then copy to 'stream'.
    }
}
