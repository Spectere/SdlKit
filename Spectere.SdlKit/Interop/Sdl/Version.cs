using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl;

/// <summary>
/// Contains the necessary constants and function imports from SDL_version.h.
/// </summary>
public class Version {
    /// <summary>
    /// Get the version of SDL that the running assembly is interacting with.
    /// </summary>
    /// <param name="ver">A <see cref="Support.Version.Version"/> structure that this function should fill in.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetVersion", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void GetVersion(out Support.Version.Version ver);
}
