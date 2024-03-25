namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when an image format conversion process fails. 
/// </summary>
public class SdlFormatConversionException : Exception {
    public SdlFormatConversionException(string message) : base(message) { }
    public SdlFormatConversionException(string message, Exception innerException) : base(message, innerException) { }
}
