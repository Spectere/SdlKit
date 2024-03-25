namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when the SDL window fails to initialize.
/// </summary>
public class SdlWindowInitializationException : Exception {
    public SdlWindowInitializationException(string message) : base(message) {}
    public SdlWindowInitializationException(string message, Exception innerException) : base(message, innerException) {}
}
