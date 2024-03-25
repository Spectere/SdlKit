using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Video;

/// <summary>
/// Represents an SDL_Window instance.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct SdlWindow {
    private readonly IntPtr _sdlWindow;

    /// <summary>
    /// If <c>true</c>, this <see cref="SdlWindow"/> is <c>null</c> and should not be used.
    /// </summary>
    public bool IsNull => _sdlWindow.Equals(IntPtr.Zero);
}
