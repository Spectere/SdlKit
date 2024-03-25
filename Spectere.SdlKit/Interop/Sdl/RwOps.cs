using Spectere.SdlKit.Interop.Sdl.Support.RwOps;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl;

/// <summary>
/// Contains the necessary constants and function imports from SDL_rwops.h.
/// </summary>
internal static class RwOps {
    /// <summary>
    /// Open a file for reading. The file must exist.
    /// </summary>
    internal const string ModeReadOnly = "r";
    
    /// <summary>
    /// Create an empty file for writing. If a file with the same name already exists it will be overwritten.
    /// </summary>
    internal const string ModeWriteOnly = "w";
    
    /// <summary>
    /// Appends to a file. Writing operations append data at the end of the file. The file is created if it does not
    /// exist.
    /// </summary>
    internal const string ModeAppendOnly = "a";
    
    /// <summary>
    /// Open a file for both reading and writing. The file must exist.
    /// </summary>
    internal const string ModeReadWrite = "r+";
    
    /// <summary>
    /// Create an empty file for both reading and writing. If a file with the same name already exists it will be
    /// overwritten.
    /// </summary>
    internal const string ModeReadWriteOverwrite = "w+";
    
    /// <summary>
    /// Open a file for both reading and writing with operations being performed at the end of the file by default.
    /// The read/write pointer can be moved using other functions if desired. The file will be created if it does
    /// not exist.
    /// </summary>
    internal const string ModeReadWriteCreate = "a+";

    /// <summary>
    /// Closes and frees an allocated <see cref="SdlRwOps"/> structure.
    /// </summary>
    /// <param name="context">The <see cref="SdlRwOps"/> structure to close.</param>
    /// <returns>0 on success, or a negative error code on failure; call <see cref="Error.GetError"/> for more
    /// information.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RWclose", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int RwClose(ref SdlRwOps context);
    
    /// <summary>
    /// Creates a read-only memory buffer for use with <see cref="RwOps"/>.
    /// </summary>
    /// <param name="mem">An <see cref="IntPtr"/> referencing a buffer to feed an <see cref="SdlRwOps"/> stream.</param>
    /// <param name="size">The buffer size, in bytes.</param>
    /// <returns>An <see cref="SdlRwOps"/> object representing this memory buffer.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RWFromConstMem", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlRwOps RwFromConstMem(IntPtr mem, int size);

    /// <summary>
    /// Creates a new <see cref="SdlRwOps"/> structure for reading from and/or writing to a named file.
    /// </summary>
    /// <param name="file">The filename to open.</param>
    /// <param name="mode">An ASCII string representing the mode to be used for opening the file. Several constants
    /// are provided in the <see cref="RwOps"/> class (<see cref="ModeReadWrite"/>, etc.).</param>
    /// <returns>An <see cref="SdlRwOps"/> object representing this file handle.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RWFromFile", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlRwOps RwFromFile(string file, string mode);

    /// <summary>
    /// Creates a new read-write memory buffer for use with <see cref="RwOps"/>.
    /// </summary>
    /// <param name="mem">An <see cref="IntPtr"/> referencing a buffer to feed an <see cref="SdlRwOps"/> stream.</param>
    /// <param name="size">The buffer size, in bytes.</param>
    /// <returns>An <see cref="SdlRwOps"/> object representing this memory buffer.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RWFromMem", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlRwOps RwFromMem(IntPtr mem, int size);
    
    /// <summary>
    /// This function seeks to byte <paramref name="offset"/>, relative to <paramref name="whence"/>.
    /// </summary>
    /// <param name="context">The <see cref="SdlRwOps"/> to manipulate.</param>
    /// <param name="offset">An offset in bytes, relative to <paramref name="whence"/>; can be negative.</param>
    /// <param name="whence">The position to seek from.</param>
    /// <returns>The final offset in the data stream after the seek, or -1 on error.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RWseek", CallingConvention = CallingConvention.Cdecl)]
    internal static extern long RwSeek(SdlRwOps context, long offset, SeekOrigin whence);
    
    /// <summary>
    /// Gets the size of the data stream in an <see cref="SdlRwOps"/>.
    /// </summary>
    /// <param name="context">The <see cref="SdlRwOps"/> to get the size of the data stream from.</param>
    /// <returns>The size of the data stream in the <see cref="SdlRwOps"/> on success, -1 if unknown, or a negative
    /// error code on failure. Call <see cref="Error.GetError"/> for more information.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RWsize", CallingConvention = CallingConvention.Cdecl)]
    internal static extern long RwSize(SdlRwOps context);

    /// <summary>
    /// Determines the current read/write offset in an <see cref="SdlRwOps"/> data stream.
    /// </summary>
    /// <param name="context">The <see cref="SdlRwOps"/> data stream to query.</param>
    /// <returns>The current offset in the stream, or -1 if the information cannot be determined.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RWtell", CallingConvention = CallingConvention.Cdecl)]
    internal static extern long RwTell(SdlRwOps context);
}
