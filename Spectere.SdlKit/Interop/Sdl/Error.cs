using Spectere.SdlKit.Interop.Sdl.Support.Error;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl;

/// <summary>
/// Contains the necessary constants and function imports from SDL_error.h.
/// </summary>
internal static class Error {
    /// <summary>
    /// Clear any previous error message for this thread.
    /// </summary>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_ClearError", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void ClearError();

    /// <summary>
    /// Throws an error based on a pre-defined <see cref="ErrorCode"/>.
    /// </summary>
    /// <param name="code">An <see cref="ErrorCode"/> indicating which error to throw.</param>
    /// <returns>Always returns -1.</returns>
    /// <remarks>This function is undocumented.</remarks>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_Error", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SdlError(ErrorCode code);
    
    /// <summary>
    /// Retrieve a message about the last error that occurred on the current thread. It is possible for multiple
    /// errors to occur before calling <see cref="GetError"/>. Only the last error is returned.
    /// </summary>
    /// <returns>A message with information about the specific error that occurred, or an empty string if there
    /// hasn't been an error message set since the last call to <see cref="ClearError"/>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetError", CallingConvention = CallingConvention.Cdecl)]
    internal static extern string GetError();

    /// <summary>
    /// Get the last error message that was set for the current thread.
    /// </summary>
    /// <param name="errStr">A buffer to fill with the last error message that was set for the current thread.</param>
    /// <param name="maxLen">The size of the buffer pointed to by the <paramref name="errStr"/> parameter.</param>
    /// <returns>The pointer passed in as the <paramref name="errStr"/> parameter.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetErrorMsg", CallingConvention = CallingConvention.Cdecl)]
    internal static extern unsafe char* GetErrorMsg(char* errStr, int maxLen);
    
    /// <summary>
    /// Set the SDL error message for the current thread. Calling this function will replace any previous error
    /// message that was set. This function always returns -1.
    /// </summary>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <returns>Always returns -1.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetError", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SetError(string fmt, __arglist);
}
