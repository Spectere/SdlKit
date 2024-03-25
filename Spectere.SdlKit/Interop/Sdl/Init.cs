using Spectere.SdlKit.Interop.Sdl.Support.Init;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl; 

/// <summary>
/// Contains the necessary constants and function imports from SDL.h.
/// </summary>
internal static class Init {
    /// <summary>
    /// This function initializes the subsystems specified by <paramref name="flags"/>.
    /// </summary>
    /// <param name="flags">Flags indicating which subsystem or subsystems to initialize.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_Init", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int Initialize(SubsystemFlags flags);

    /// <summary>
    /// This function initializes specific SDL subsystems.
    ///
    /// Subsystem initialization is ref-counted, you must call SDL_QuitSubSystem() for each SDL_InitSubSystem() to
    /// correctly shutdown a subsystem manually(or call SDL_Quit() to force shutdown). If a subsystem is already loaded
    /// then this call will increase the ref-count and return.
    /// </summary>
    /// <param name="flags">Flags indicating which subsystem or subsystems to initialize.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_InitSubSystem", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int InitSubSystem(SubsystemFlags flags);

    /// <summary>
    /// This function cleans up specific SDL subsystems.
    /// </summary>
    /// <param name="flags">Flags indicating which subsystem or subsystems to quit.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_QuitSubSystem", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int QuitSubSystem(SubsystemFlags flags);

    /// <summary>
    /// <para>
    /// This function returns a mask of the specified subsystems which have previously been initialized.
    /// </para>
    /// <para>
    /// If <paramref name="flags"/> is 0, it returns a mask of all initialized subsystems.
    /// </para>
    /// </summary>
    /// <param name="flags">Flags indicating which subsystem or subsystems to query.</param>
    /// <returns>A set of all initialized subsystems.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_WasInit", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SubsystemFlags WasInit(SubsystemFlags flags);

    /// <summary>
    /// This function cleans up all initialized subsystems. You should call it upon all exit conditions.
    /// </summary>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_Quit", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void Quit();
}
