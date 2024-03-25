using Spectere.SdlKit.Interop.Sdl.Support.Surface;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl;

/// <summary>
/// Contains the necessary constants and function imports from SDL_surface.h.
/// </summary>
public class Surface {
    /// <summary>
    /// Copy an existing surface to a new surface of the specified format.
    /// </summary>
    /// <param name="source">The existing <see cref="SdlSurface"/> to convert.</param>
    /// <param name="format">The SDL pixel format that the new surface is optimized for.</param>
    /// <param name="unused">This is unused and should be left at 0. This is a leftover from SDL 1.2's API.</param>
    /// <returns>The new <see cref="SdlSurface"/> that is created, or <c>null</c> if it fails.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_ConvertSurfaceFormat", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface ConvertSurfaceFormat(SdlSurface source, uint format, uint unused = 0);

    /// <summary>
    /// Free an RGB surface. It is safe to pass <c>null</c> to this function.
    /// </summary>
    /// <param name="surface">The <see cref="SdlSurface"/> to free.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_FreeSurface", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void FreeSurface(SdlSurface surface);
}
