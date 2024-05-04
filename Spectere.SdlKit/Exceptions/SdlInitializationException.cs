using Spectere.SdlKit.Interop.Sdl.Support.Init;

namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when an SDL subsystem fails to initialize.
/// </summary>
public class SdlInitializationException : Exception {
    internal SdlInitializationException(SubsystemFlags subsystems, string message)
        : base($"Unable to initialize subsystem(s): {subsystems}\nError: {message}") {}
    internal SdlInitializationException(SubsystemFlags subsystems, string message, Exception innerException)
        : base($"Unable to initialize subsystem(s): {subsystems}\nError: {message}", innerException) {}
    public SdlInitializationException(string message) : base(message) {}
    public SdlInitializationException(string message, Exception innerException) : base(message, innerException) {}
}
