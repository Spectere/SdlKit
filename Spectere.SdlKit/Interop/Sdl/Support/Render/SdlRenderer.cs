using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Render;

/// <summary>
/// Represents an SDL_Renderer instance.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct SdlRenderer {
    private readonly IntPtr _sdlRenderer;

    /// <summary>
    /// If <c>true</c>, this <see cref="SdlRenderer"/> is <c>null</c> and should not be used.
    /// </summary>
    public bool IsNull => _sdlRenderer.Equals(IntPtr.Zero);
}
