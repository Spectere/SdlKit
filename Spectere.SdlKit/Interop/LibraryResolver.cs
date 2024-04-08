using System.Reflection;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop;

/// <summary>
/// Contains helper methods related to native library resolution.
/// </summary>
internal static class LibraryResolver {
    /// <summary>
    /// If this is <c>true</c>, the custom resolver has already been set. Future calls to
    /// <see cref="SetSdlKitLibraryResolver"/> will immediately return.
    /// </summary>
    private static bool _resolverSet;

    /// <summary>
    /// Configures the .NET runtime to use SDLKit's native library resolver. This must be called prior to calling any
    /// native functions that are marked as requiring this resolver.
    /// </summary>
    internal static void SetSdlKitLibraryResolver() {
        if(_resolverSet) {
            return;
        }
        
        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), SdlKitDllImportResolver);
        _resolverSet = true;
    }

    /// <summary>
    /// SDLKit's custom DllImport resolver. This is required to handle cases where required libraries are named
    /// differently on different operating systems (for example, the C runtime being called "msvcrt" on Windows and
    /// "libc" on Linux/Unix systems.
    /// </summary>
    /// <param name="libraryName">The name of the native library to load.</param>
    /// <param name="assembly">The assembly loading the native library.</param>
    /// <param name="searchPath">The search path.</param>
    /// <returns>The OS handle for the loaded library.</returns>
    private static IntPtr SdlKitDllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath) {
        if(libraryName == "c") {
            // libc is "msvcrt" on Windows.
            if(OperatingSystem.IsWindows()) {
                return NativeLibrary.Load("msvcrt", assembly, searchPath);
            }
        }
        
        // Fall back to the default resolver.
        return IntPtr.Zero;
    }
}
