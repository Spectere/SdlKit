namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when SDL is unable to create a texture.
/// </summary>
public class SdlTextureInitializationException : Exception {
    public SdlTextureInitializationException(string message) : base(message) { }
    public SdlTextureInitializationException(string message, Exception innerException) : base(message, innerException) { }
}
