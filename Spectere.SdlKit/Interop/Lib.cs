namespace Spectere.SdlKit.Interop {
    /// <summary>
    /// Contains .NET constants to assist with making calls to the SDL library.
    /// </summary>
    internal static class Lib {
        /// <summary>
        /// Contains the name of a POSIX-compliant libc implementation.
        /// </summary>
        internal const string LibC = "c";
        
        /// <summary>
        /// Contains the name of the Windows NT kernel.
        /// </summary>
        internal const string NtKernel = "kernel32.dll";
        
        /// <summary>
        /// Contains the name of the SDL library.
        /// </summary>
        internal const string Sdl2 = "SDL2";

        /// <summary>
        /// Contains the name of the SDL_image library.
        /// </summary>
        internal const string Sdl2Image = "SDL2_image";
    }
}
