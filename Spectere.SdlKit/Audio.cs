using Spectere.SdlKit.Exceptions;
using SdlAudio = Spectere.SdlKit.Interop.Sdl.Audio;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.Init;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit;

/// <summary>
/// Provides a means for an application to play audio via SDL.
/// </summary>
public class Audio {
    /// <summary>
    /// The audio buffer that the application should fill in for us.
    /// </summary>
    private float[] _buffer = [];

    /// <summary>
    /// The audio buffer that SDLKit will send back if format conversions must occur.
    /// </summary>
    private byte[] _convertedAudio = [];

    /// <summary>
    /// If this is <c>true</c>, the system's endianness matches that of the currently open audio format.
    /// </summary>
    private bool _endianMatches;

    /// <summary>
    /// An SDL device ID.
    /// </summary>
    private uint _sdlAudioDeviceId;
    
    /// <summary>
    /// A field that will point to the audio callback function in this class.
    /// </summary>
    private Interop.Sdl.Audio.SdlAudioCallback _callback;
    
    /// <summary>
    /// Raised when the audio subsystem is ready to receive more audio.
    /// </summary>
    public delegate void AudioRequestedEventHandler(object sender, int samplesRequested, in float[] buffer);
    
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
    /// Gets the current audio specs. If this audio device is not open, this will be <c>null</c>.
    /// </summary>
    public AudioSpec? CurrentAudioSpec { get; private set; }
    
    /// <summary>
    /// Gets whether or not the SDL subsystem was initialized. This should set to <c>true</c> after the device list
    /// is queried or an audio device is opened, then reset to <c>false</c> when the audio device is closed.
    /// </summary>
    public bool Initialized { get; private set; }

    /// <summary>
    /// Gets whether or not this audio device is open. This will be set to <c>true</c> when an audio device is opened,
    /// otherwise it will be <c>false</c>.
    /// </summary>
    public bool Opened => _sdlAudioDeviceId > 0;

    /// <summary>
    /// Creates a new SDLKit audio interface. Note that the audio subsystem must subsequently be opened using
    /// <see cref="Open()"/> before audio can be played.
    /// </summary>
    public Audio() {
        _callback = SdlCallback;
    }

    /// <summary>
    /// Attempts to gracefully clean up the audio subsystem.
    /// </summary>
    ~Audio() {
        Close();
    }

    /// <summary>
    /// Closes the audio interface.
    /// </summary>
    public void Close() {
        if(Opened) {
            SdlAudio.CloseAudioDevice(_sdlAudioDeviceId);
            _sdlAudioDeviceId = 0;
        }
        
        Shutdown();
    }
    
    /// <summary>
    /// Converts the audio buffer to a 8-bit integer format.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ConvertToInt8() {
        var unsignedConversion = CurrentAudioSpec?.IsSigned is null or false ? 0x80 : 0;
        for(var i = 0; i < _buffer.Length; i++) {
            var newSample = (byte)(_buffer[i] * 127);
            _convertedAudio[i] = (byte)(newSample ^ unsignedConversion);
        }
    }

    /// <summary>
    /// Converts the audio buffer to a 16-bit integer format.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ConvertToInt16() {
        var unsignedConversion = CurrentAudioSpec?.IsSigned is null or false ? 0x80 : 0;
        if(_endianMatches) {
            for(var i = 0; i < _buffer.Length; i++) {
                var newSample = (short)(_buffer[i] * short.MaxValue);
                _convertedAudio[i * 2] = (byte)(newSample);
                _convertedAudio[i * 2 + 1] = (byte)((newSample >> 8) ^ unsignedConversion);
            }
        } else {
            for(var i = 0; i < _buffer.Length; i++) {
                var newSample = (short)(_buffer[i] * short.MaxValue);
                _convertedAudio[i * 2] = (byte)((newSample >> 8) ^ unsignedConversion);
                _convertedAudio[i * 2 + 1] = (byte)(newSample);
            }
        }
    }

    /// <summary>
    /// Converts the audio buffer to a 32-bit integer format.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ConvertToInt32() {
        const int peakReduction = 128;  // HACK: The audio seems to distort if this isn't applied. I don't get it.
        if(_endianMatches) {
            for(var i = 0; i < _buffer.Length; i++) {
                var newSample = (int)(_buffer[i] * int.MaxValue - peakReduction);
                _convertedAudio[i * 4] = (byte)(newSample);
                _convertedAudio[i * 4 + 1] = (byte)(newSample >> 8);
                _convertedAudio[i * 4 + 2] = (byte)(newSample >> 16);
                _convertedAudio[i * 4 + 3] = (byte)(newSample >> 24);
            }
        } else {
            for(var i = 0; i < _buffer.Length; i++) {
                var newSample = (int)(_buffer[i] * int.MaxValue - peakReduction);
                _convertedAudio[i * 4] = (byte)(newSample >> 24);
                _convertedAudio[i * 4 + 1] = (byte)(newSample >> 16);
                _convertedAudio[i * 4 + 2] = (byte)(newSample >> 8);
                _convertedAudio[i * 4 + 3] = (byte)(newSample);
            }
        }
    }

    /// <summary>
    /// Queries the audio devices in the system and returns the results as a list.
    /// </summary>
    /// <returns>A list of audio devices that are currently plugged in and operational.</returns>
    public List<AudioDevice> GetAudioDevices() {
        var audioDevices = new List<AudioDevice>();
        var numAudioDevices = SdlAudio.GetNumAudioDevices(0);

        for(var i = 0; i < numAudioDevices; i++) {
            var name = SdlAudio.GetAudioDeviceName(i, 0);
            var result = SdlAudio.GetAudioDeviceSpec(i, 0, out var sdlAudioSpec);
            if(result != 0) {
                continue;
            }
            
            audioDevices.Add(new AudioDevice(name, new AudioSpec(sdlAudioSpec)));
        }

        return audioDevices;
    }
    
    /// <summary>
    /// Initializes the SDL audio subsystem.
    /// </summary>
    /// <exception cref="SdlAudioDeviceOpenException">Thrown if the audio device could not be opened.</exception>
    private void Initialize() {
        if(Init.InitSubSystem(SubsystemFlags.Audio) != 0) {
            var message = Error.GetError();
            throw new SdlInitializationException(SubsystemFlags.Audio, message);
        }

        Initialized = true;
    }
    
    /// <summary>
    /// Opens the default audio device with the default specs.
    /// </summary>
    /// <remarks>In order for the audio system to be enabled, the <see cref="SetPlaybackState"/> method must be called
    /// with its <c>playbackState</c> parameter being set to <see cref="AudioState.Unpaused"/>.</remarks>
    /// <exception cref="SdlAudioDeviceOpenException">Thrown if the audio device could not be opened.</exception>
    public void Open() =>
        Open((string?)null, new AudioSpec());
    
    /// <summary>
    /// Opens the default audio device with the given specs.
    /// </summary>
    /// <param name="requestedSpec">The requested audio spec. Note that if <paramref name="allowedAudioSpecChanges"/>
    /// is set to anything other than <see cref="AllowedAudioSpecChanges.DisallowChange"/>, some or all of these
    /// settings may be changed. See <see cref="CurrentAudioSpec"/> for the settings that SDL opened the device with.</param>
    /// <param name="allowedAudioSpecChanges">Indicates which, if any, of the settings in <paramref name="requestedSpec"/>
    /// may be changed to ensure that the audio device can be opened. This defaults to <see cref="AllowedAudioSpecChanges.AllowAnyChange"/>,
    /// which is highly recommended.</param>
    /// <remarks>In order for the audio system to be enabled, the <see cref="SetPlaybackState"/> method must be called
    /// with its <c>playbackState</c> parameter being set to <see cref="AudioState.Unpaused"/>.</remarks>
    /// <exception cref="SdlAudioDeviceOpenException">Thrown if the audio device could not be opened.</exception>
    public void Open(AudioSpec requestedSpec, AllowedAudioSpecChanges allowedAudioSpecChanges = AllowedAudioSpecChanges.AllowAnyChange) =>
        Open((string?)null, requestedSpec, allowedAudioSpecChanges);

    /// <summary>
    /// Opens an audio device.
    /// </summary>
    /// <param name="deviceName">The name of the device to open, or <c>null</c> to open the default audio device.</param>
    /// <param name="requestedSpec">The requested audio spec. Note that if <paramref name="allowedAudioSpecChanges"/>
    /// is set to anything other than <see cref="AllowedAudioSpecChanges.DisallowChange"/>, some or all of these
    /// settings may be changed. See <see cref="CurrentAudioSpec"/> for the settings that SDL opened the device with.</param>
    /// <param name="allowedAudioSpecChanges">Indicates which, if any, of the settings in <paramref name="requestedSpec"/>
    /// may be changed to ensure that the audio device can be opened. This defaults to <see cref="AllowedAudioSpecChanges.AllowAnyChange"/>,
    /// which is highly recommended.</param>
    /// <remarks>In order for the audio system to be enabled, the <see cref="SetPlaybackState"/> method must be called
    /// with its <c>playbackState</c> parameter being set to <see cref="AudioState.Unpaused"/>.</remarks>
    /// <exception cref="SdlAudioDeviceOpenException">Thrown if the audio device could not be opened.</exception>
    public void Open(string? deviceName, AudioSpec requestedSpec, AllowedAudioSpecChanges allowedAudioSpecChanges = AllowedAudioSpecChanges.AllowAnyChange) {
        Initialize();

        _callback = SdlCallback;
        
        var requestedAudioSpec = requestedSpec.ToSdlAudioSpec();
        requestedAudioSpec.Callback = _callback;
        
        _sdlAudioDeviceId = SdlAudio.OpenAudioDeviceManaged(deviceName, 0, ref requestedAudioSpec, out var obtainedAudioSpec, allowedAudioSpecChanges);
        if(_sdlAudioDeviceId == 0) {
            Shutdown();
            var message = Error.GetError();
            throw new SdlAudioDeviceOpenException(message);
        }

        CurrentAudioSpec = new AudioSpec(obtainedAudioSpec);

        var systemEndian = BitConverter.IsLittleEndian ? Endian.LittleEndian : Endian.BigEndian;
        _endianMatches = systemEndian == CurrentAudioSpec.Endianness;
    }

    /// <summary>
    /// Opens an audio device.
    /// </summary>
    /// <param name="audioDevice">An <see cref="AudioDevice"/> to open. This will attempt to use the specs in
    /// <see cref="AudioDevice.PreferredSpec"/> will be used.</param>
    /// <remarks>In order for the audio system to be enabled, the <see cref="SetPlaybackState"/> method must be called
    /// with its <c>playbackState</c> parameter being set to <see cref="AudioState.Unpaused"/>.</remarks>
    /// <exception cref="SdlAudioDeviceOpenException">Thrown if the audio device could not be opened.</exception>
    public void Open(AudioDevice audioDevice) =>
        Open(audioDevice.Name, audioDevice.PreferredSpec);
    
    /// <summary>
    /// Opens an audio device.
    /// </summary>
    /// <param name="audioDevice">An <see cref="AudioDevice"/> to open. This will attempt to use the specs in
    /// <see cref="AudioDevice.PreferredSpec"/> will be used.</param>
    /// <param name="requestedSpec">The requested audio spec. Note that if <paramref name="allowedAudioSpecChanges"/>
    /// is set to anything other than <see cref="AllowedAudioSpecChanges.DisallowChange"/>, some or all of these
    /// settings may be changed. See <see cref="CurrentAudioSpec"/> for the settings that SDL opened the device with.</param>
    /// <param name="allowedAudioSpecChanges">Indicates which, if any, of the settings in <paramref name="requestedSpec"/>
    /// may be changed to ensure that the audio device can be opened. This defaults to <see cref="AllowedAudioSpecChanges.AllowAnyChange"/>,
    /// which is highly recommended.</param>
    /// <remarks>In order for the audio system to be enabled, the <see cref="SetPlaybackState"/> method must be called
    /// with its <c>playbackState</c> parameter being set to <see cref="AudioState.Unpaused"/>.</remarks>
    /// <exception cref="SdlAudioDeviceOpenException">Thrown if the audio device could not be opened.</exception>
    public void Open(AudioDevice audioDevice, AudioSpec requestedSpec, AllowedAudioSpecChanges allowedAudioSpecChanges = AllowedAudioSpecChanges.AllowAnyChange) =>
        Open(audioDevice.Name, requestedSpec, allowedAudioSpecChanges);

    /// <summary>
    /// Sets the audio stream's playback state. If this is called before an audio device is opened, nothing will
    /// happen.
    /// </summary>
    /// <param name="playbackState">The value that the audio stream's playback state should be set to.</param>
    public void SetPlaybackState(AudioState playbackState) {
        if(!Initialized || !Opened) {
            return;
        }
        
        SdlAudio.PauseAudioDevice(_sdlAudioDeviceId, playbackState);
    }

    /// <summary>
    /// The SDL audio callback function.
    /// </summary>
    /// <param name="userdata">Custom user data. This is not used by SdlKit.</param>
    /// <param name="stream">A pointer to the audio stream that should be filled by the application.</param>
    /// <param name="len">The amount of data to fill the stream with, in bytes.</param>
    private unsafe void SdlCallback(IntPtr userdata, IntPtr stream, int len) {
        if(AudioRequested is null || CurrentAudioSpec is null) {
            // No subscriber. Fill the stream with silence.
            SdlMemory.Memset(stream.ToPointer(), 0, (nuint)len);
            return;
        }

        // Resize the arrays if necessary.
        var samples = len / (CurrentAudioSpec.BitSize / 8);
        if(_buffer.Length != samples) {
            _buffer = GC.AllocateUninitializedArray<float>(samples);
            
            if(!CurrentAudioSpec.IsFloat) {
                _convertedAudio = GC.AllocateUninitializedArray<byte>(len);
            }
        }
        
        // Flush the buffer if requested.
        if(ClearBufferOnCallback) {
            Array.Clear(_buffer);
        }
        
        // Raise an event with the target application.
        AudioRequested(this, samples, _buffer);
        
        // Convert the audio stream from float to the desired format, then copy to 'stream'.
        if(CurrentAudioSpec.IsFloat) {
            Marshal.Copy(_buffer, 0, stream, samples);
            return;
        }

        switch(CurrentAudioSpec.BitSize) {
            case 8: ConvertToInt8(); break;
            case 16: ConvertToInt16(); break;
            case 32: ConvertToInt32(); break;
        }
        Marshal.Copy(_convertedAudio, 0, stream, len);
    }
    
    /// <summary>
    /// Shuts down the SDL audio subsystem.
    /// </summary>
    private void Shutdown() {
        if(!Initialized) {
            return;
        }

        _ = Init.QuitSubSystem(SubsystemFlags.Audio);
    }
}
