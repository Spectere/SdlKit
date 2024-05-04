namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when the application specifies an unsupported audio format.
/// </summary>
public class SdlUnsupportedAudioFormat : Exception {
    internal SdlUnsupportedAudioFormat(int bitSize, bool isFloat, bool isSigned) :
        base($"Unsupported audio format specification ({bitSize}-bit {(isFloat ? "float" : "int")}, {(isSigned ? "signed" : "unsigned")})") {}
    internal SdlUnsupportedAudioFormat(int bitSize, bool isFloat, bool isSigned, Exception innerException) :
        base($"Unsupported audio format specification ({bitSize}-bit {(isFloat ? "float" : "int")}, {(isSigned ? "signed" : "unsigned")})",
            innerException) {}
}
