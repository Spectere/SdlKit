namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when SDL_image is unable to load an image.
/// </summary>
public class SdlImageLoadException : Exception {
    public SdlImageLoadException(string message) : base(message) { }
    public SdlImageLoadException(string message, Exception innerException) : base(message, innerException) { }
}
