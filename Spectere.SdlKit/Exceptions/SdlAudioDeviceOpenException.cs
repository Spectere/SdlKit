namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when SDL is unable to open an audio device.
/// </summary>
public class SdlAudioDeviceOpenException : Exception {
    public SdlAudioDeviceOpenException(string message) : base(message) { }
    public SdlAudioDeviceOpenException(string message, Exception innerException) : base(message, innerException) { }
}
