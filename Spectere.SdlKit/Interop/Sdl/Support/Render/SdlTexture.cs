using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Render;

/// <summary>
/// Represents an SDL_Texture instance.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct SdlTexture {
    private readonly IntPtr _sdlTexture;
    
    /// <summary>
    /// If <c>true</c>, this <see cref="SdlTexture"/> is <c>null</c> and should not be used.
    /// </summary>
    public bool IsNull => _sdlTexture.Equals(IntPtr.Zero);
}
