using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.GameController;

/// <summary>
/// Represents an SDL_GameController instance.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct SdlGameController {
    private readonly IntPtr _sdlGameController;

    /// <summary>
    /// If <c>true</c>, this <see cref="SdlGameController"/> is <c>null</c> and should not be used.
    /// </summary>
    public bool IsNull => _sdlGameController.Equals(IntPtr.Zero);
}
