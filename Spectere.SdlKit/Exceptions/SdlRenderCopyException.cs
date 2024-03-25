using Spectere.SdlKit.Interop.Sdl;

namespace Spectere.SdlKit.Exceptions;

/// <summary>
/// Thrown when SDL's <see cref="Render.RenderCopy"/> or <see cref="Render.RenderCopyEx"/> calls fail. This is typically
/// only thrown when debugging the library.
/// </summary>
public class SdlRenderCopyException : Exception {
    public SdlRenderCopyException(string message) : base(message) {}
    public SdlRenderCopyException(string message, Exception innerException) : base(message, innerException) {}
}
