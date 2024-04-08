using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.LibC;

/// <summary>
/// Contains C library functions related to memory.
/// </summary>
internal class CMemory {
    /// <summary>
    /// Fill memory with a constant byte.
    /// </summary>
    /// <param name="s">A pointer to the memory area to fill.</param>
    /// <param name="c">The character to fill the block of memory with.</param>
    /// <param name="n">The number of bytes to fill.</param>
    /// <returns>A pointer to the memory area <paramref name="s"/>.</returns>
    // Possible caveat: nuint isn't always equal to size_t.
    [DllImport(Lib.LibC, EntryPoint = "memset", CallingConvention = CallingConvention.Cdecl)]
    internal static extern unsafe void* Memset(void* s, int c, nuint n);
}
