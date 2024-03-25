using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl; 

/// <summary>
/// Functions taken from SDL_hints.h. Hints (and their associated values) can be found in the
/// <see cref="Spectere.SdlKit.Interop.Sdl.Support.Hints"/> namespace.
/// </summary>
internal static class Hints {
    /// <summary>
    /// Gets the value of a hint.
    /// </summary>
    /// <param name="name">The name of the hint to set.</param>
    /// <returns><c>true</c> if the hint was set, otherwise <c>false</c>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetHint", CallingConvention = CallingConvention.Cdecl)]
    internal static extern string GetHint(string name);

    /// <summary>
    /// Sets a hint with normal priority.
    /// </summary>
    /// <param name="name">The name of the hint to set.</param>
    /// <param name="value">The value to set the hint to.</param>
    /// <returns><c>true</c> if the hint was set, otherwise <c>false</c>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetHint", CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SetHint(string name, string value);
}
