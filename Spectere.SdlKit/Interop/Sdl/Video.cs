using Spectere.SdlKit.Interop.Sdl.Support.Surface;
using Spectere.SdlKit.Interop.Sdl.Support.Video;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl; 

/// <summary>
/// Contains the necessary constants and function imports from SDL_video.h.
/// </summary>
internal static class Video {
    /// <summary>
    /// Indicates that the window should be in the center of the screen. To center the window on a specific display, use the <see cref="WindowPosCenteredDisplay"/> function.
    /// </summary>
    private const int WindowPosCentered = 0x2FFF0000;

    /// <summary>
    /// Calculates a value that allows the window to be placed in the center of a specified display.
    /// </summary>
    /// <param name="display">The index of the display to place the window on.</param>
    /// <returns>A coordinate value that should be passed to <see cref="CreateWindow"/>.</returns>
    public static int WindowPosCenteredDisplay(uint display) => (int)(WindowPosCentered | display);
    
    /// <summary>Indicates that the window manager should position the window. To place the window on a specific display, use the <see cref="WindowPosCenteredDisplay"/> function.</summary>
    private const int WindowPosUndefined = 0x1FFF0000;
    
    /// <summary>
    /// Calculates a value that allows the window to be placed on a specific display, with its exact position determined by the window manager.
    /// </summary>
    /// <param name="display">The index of the display to place the window on.</param>
    /// <returns>A coordinate value that should be passed to <see cref="CreateWindow"/>.</returns>
    public static int WindowPosUndefinedDisplay(uint display) => (int)(WindowPosUndefined | display);
    
    /// <summary>
    /// Creates a window with the specified position, dimensions, and flags.
    /// </summary>
    /// <param name="title">The title of the window, in UTF-8 encoding.</param>
    /// <param name="x">The x position of the window, <see cref="WindowPosCentered"/>, or <see cref="WindowPosUndefined"/>.</param>
    /// <param name="y">The y position of the window, <see cref="WindowPosCentered"/>, or <see cref="WindowPosUndefined"/>.</param>
    /// <param name="w">The width of the window, in screen coordinates.</param>
    /// <param name="h">The height of the window, in screen coordinates.</param>
    /// <param name="flags">One or more <see cref="WindowFlags"/> OR'd together.</param>
    /// <returns>The window that was created, or NULL on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_CreateWindow", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlWindow CreateWindow(string title, int x, int y, int w, int h, WindowFlags flags);

    /// <summary>
    /// Destroys an SDL window.
    /// </summary>
    /// <param name="window">The window to destroy.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_DestroyWindow", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void DestroyWindow(SdlWindow window);

    /// <summary>
    /// Prevents the screen from being blanked by a screen saver. If you disable the screensaver, it is automatically
    /// re-enabled when SDL quits.
    /// </summary>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_DisableScreenSaver", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void DisableScreenSaver();

    /// <summary>
    /// Allows the screen to be blanked by a screen saver.
    /// </summary>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_EnableScreenSaver", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void EnableScreenSaver();
    
    /// <summary>
    /// Request a window to demand attention from the user.
    /// </summary>
    /// <param name="window">The window to be flashed.</param>
    /// <param name="operation">The flash operation.</param>
    /// <returns>0 on success or a negative error code on failure; call <see cref="Error.GetError"/> for more information.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_FlashWindow", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int FlashWindow(SdlWindow window, FlashOperation operation);

    /// <summary>
    /// Get the number of video drivers compiled into SDL.
    /// </summary>
    /// <returns>A number >= 1 on success or a negative error code on failure; call <see cref="Error.GetError"/> for more information.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetNumVideoDrivers", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int GetNumVideoDrivers();

    /// <summary>
    /// Gets the name of a built-in video driver.
    /// </summary>
    /// <param name="index">The index of a video driver.</param>
    /// <returns>The name of the video driver with the given <paramref name="index"/>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetVideoDriver", CallingConvention = CallingConvention.Cdecl)]
    internal static extern string GetVideoDriver(int index);
    
    /// <summary>
    /// Get the window flags.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>A mask of the <see cref="WindowFlags"/> associated with <param name="window"/>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetWindowFlags", CallingConvention = CallingConvention.Cdecl)]
    internal static extern WindowFlags GetWindowFlags(SdlWindow window);

    /// <summary>
    /// Gets the numeric ID of a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The ID of the window.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetWindowID", CallingConvention = CallingConvention.Cdecl)]
    internal static extern uint GetWindowId(SdlWindow window);

    /// <summary>
    /// Get the size of a window's client area. <c>NULL</c> can be passed as the <paramref name="w"/> or <paramref name="h"/>
    /// parameter if the width or height value is not desired. 
    /// </summary>
    /// <param name="window">The window to query the width and height from.</param>
    /// <param name="w">A reference filled in with the width of the window, in screen coordinates. May be <c>NULL</c>.</param>
    /// <param name="h">A reference filled in with the height of the window, in screen coordinates. May be <c>NULL</c>.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetWindowSize", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void GetWindowSize(SdlWindow window, ref int w, ref int h);

    /// <summary>
    /// Hides a window.
    /// </summary>
    /// <param name="window">The window to hide.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_HideWindow", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void HideWindow(SdlWindow window);

    /// <summary>
    /// Checks whether the screen saver is current enabled.
    /// </summary>
    /// <returns>1 if the screen saver is enabled, or 0 if it is disabled.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_IsScreenSaverEnabled", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsScreenSaverEnabled();

    /// <summary>
    /// Set a window's fullscreen state.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="flags"><see cref="WindowFlags.Fullscreen"/>, <see cref="WindowFlags.FullscreenDesktop"/>, or <see cref="WindowFlags.None"/>.</param>
    /// <returns>Returns 0 on success or a negative error code on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetWindowFullscreen", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SetWindowFullscreen(SdlWindow window, WindowFlags flags);

    /// <summary>
    /// Set the icon for a window.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="icon">An SDL surface containing the icon for the window.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetWindowIcon", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SetWindowIcon(SdlWindow window, out SdlSurface icon);
    
    /// <summary>
    /// Set the size of a window's client area.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="w">The width of the window in pixels, in screen coordinates. Must be > 0.</param>
    /// <param name="h">The height of the window in pixels, in screen coordinates. Must be > 0.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetWindowsSize", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SetWindowSize(SdlWindow window, int w, int h);

    /// <summary>
    /// Sets the title of an SDL window.
    /// </summary>
    /// <param name="window">The window to set the title of.</param>
    /// <param name="title">The string to set the window title to.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetWindowTitle", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SetWindowTitle(SdlWindow window, string title);

    /// <summary>
    /// Shows an SDL window.
    /// </summary>
    /// <param name="window">The window to show.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_ShowWindow", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void ShowWindow(SdlWindow window);

    /// <summary>
    /// Initialize the video subsystem, optionally specifying video driver.
    /// </summary>
    /// <param name="driverName">The name a video driver to initialize, or <c>null</c> for the default driver.</param>
    /// <returns>0 on success or a negative error code on failure; call <see cref="Error.GetError"/> for more information.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_VideoInit", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int VideoInit(string? driverName);

    /// <summary>
    /// Shut down the video subsystem, if initialized with <see cref="VideoInit"/>.
    /// </summary>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_VideoQuit", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void VideoQuit();
}
