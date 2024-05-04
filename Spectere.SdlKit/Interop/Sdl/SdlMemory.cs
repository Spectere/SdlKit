using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl;

/// <summary>
/// Contains some libc memory routines that are helpfully exported by the SDL library.
/// </summary>
public class SdlMemory {
    /// <summary>
    /// Frees a block of unmanaged memory.
    /// </summary>
    /// <param name="ptr">An <see cref="IntPtr"/> pointing to the block of memory to free.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_free", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void Free(IntPtr ptr);
    
    /// <summary>
    /// Fill memory with a constant byte.
    /// </summary>
    /// <param name="dst">A pointer to the memory area to fill.</param>
    /// <param name="c">The character to fill the block of memory with.</param>
    /// <param name="len">The number of bytes to fill.</param>
    /// <returns>A pointer to the memory area <paramref name="s"/>.</returns>
    // Possible caveat: nuint isn't always equal to size_t.
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_memset", CallingConvention = CallingConvention.Cdecl)]
    internal static extern unsafe void* Memset(void* dst, int c, nuint len);
}
