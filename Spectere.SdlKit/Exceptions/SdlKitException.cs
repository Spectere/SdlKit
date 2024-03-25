namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when an error within the library is detected.
/// </summary>
public class SdlKitException : Exception {
    public SdlKitException(string message)
        : base($"An exception has occurred within SdlKit!\n\n{message}") {}

    public SdlKitException(string message, Exception innerException)
        : base($"An exception has occurred within SdlKit!\n\n{message}", innerException) {}
}
