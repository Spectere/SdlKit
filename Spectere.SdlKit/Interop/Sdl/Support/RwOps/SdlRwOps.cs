using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.RwOps;

/// <summary>
/// Represents an SDL_RWops instance.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct SdlRwOps {
    private readonly IntPtr _sdlRwOps;

    /// <summary>
    /// If <c>true</c>, this <see cref="SdlRwOps"/> is <c>null</c> and should not be used.
    /// </summary>
    public bool IsNull => _sdlRwOps.Equals(IntPtr.Zero);
}
