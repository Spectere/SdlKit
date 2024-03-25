namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when the SDL renderer fails to initialize.
/// </summary>
public class SdlRendererInitializationException : Exception {
    public SdlRendererInitializationException(string message) : base(message) {}
    public SdlRendererInitializationException(string message, Exception innerException) : base(message, innerException) {}
}
