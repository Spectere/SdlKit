using Spectere.SdlKit.Interop.Sdl.Support.GameController;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl;

/// <summary>
/// Contains the necessary constants and function imports from SDL_gamecontroller.h.
/// </summary>
internal class GameController {
    /// <summary>
    /// Close a game controller previously opened with <see cref="GameControllerOpen"/>.
    /// </summary>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerClose", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void GameControllerClose(SdlGameController gameController);

    /// <summary>
    /// Gets the number of touchpads on a game controller.
    /// </summary>
    /// <param name="gameController">The controller to query.</param>
    /// <returns>The number of touchpads on this game controller.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerGetNumTouchpads", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int GameControllerGetNumTouchpads(SdlGameController gameController);
    
    /// <summary>
    /// Get the player index of an opened game controller.
    /// </summary>
    /// <param name="gameController">The controller to query.</param>
    /// <returns>The player index for this controller, or -1 if it's not available.</returns>
    /// <remarks>Added in SDL 2.0.12.</remarks>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerGetPlayerIndex", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int GameControllerGetPlayerIndex(SdlGameController gameController);

    /// <summary>
    /// Gets the type of this currently opened controller.
    /// </summary>
    /// <param name="gameController">The controller to query.</param>
    /// <returns>A <see cref="GamepadType"/> containing the type of controller that was detected.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerGetType", CallingConvention = CallingConvention.Cdecl)]
    internal static extern GamepadType GameControllerGetType(SdlGameController gameController);

    /// <summary>
    /// Query whether a game controller has a modifiable LED.
    /// </summary>
    /// <param name="gameController">The controller to query.</param>
    /// <returns><c>true</c> if the controller has a modifiable LED, otherwise <c>false</c>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerHasLED", CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool GameControllerHasLed(SdlGameController gameController);

    /// <summary>
    /// Query whether a game controller has rumble support.
    /// </summary>
    /// <param name="gameController">The controller to query.</param>
    /// <returns><c>true</c> if the controller has rumble support, otherwise <c>false</c>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerHasRumble", CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool GameControllerHasRumble(SdlGameController gameController);

    /// <summary>
    /// Query whether a game controller has rumble support on triggers.
    /// </summary>
    /// <param name="gameController">The controller to query.</param>
    /// <returns><c>true</c> if the controller has trigger rumble support, otherwise <c>false</c>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerHasRumbleTriggers", CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool GameControllerHasRumbleTriggers(SdlGameController gameController);

    /// <summary>
    /// Gets the implementation-dependent name for an opened game controller.
    /// </summary>
    /// <param name="gameController">The controller to query.</param>
    /// <returns>The implementation-dependent name for the game controller, or <c>null</c> if there is no name or the
    /// identifier passed in invalid.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerName", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr GameControllerName(SdlGameController gameController);
    
    /// <summary>
    /// Opens a game controller for use.
    /// </summary>
    /// <param name="joystickIndex">The numeric index of the device to open as a game controller.</param>
    /// <returns>A <see cref="SdlGameController"/> instance representing the game controller. If this object's
    /// <c>IsNull</c> property is <c>true</c>, an error has occurred.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerOpen", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlGameController GameControllerOpen(int joystickIndex);

    /// <summary>
    /// Starts a rumble effect on a game controller. Each call to this function cancels any previous rumble effect, and
    /// calling it with 0 intensity stops any rumbling.
    /// </summary>
    /// <param name="gameController">The controller to vibrate.</param>
    /// <param name="lowFrequencyRumbleIntensity">The intensity of the low frequency (left) rumble motor.</param>
    /// <param name="highFrequencyRumbleIntensity">The intensity of the high frequency (right) rumble motor.</param>
    /// <param name="durationMilliseconds">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>0, or -1 if rumble isn't supported on this controller.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerRumble", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int GameControllerRumble(SdlGameController gameController, ushort lowFrequencyRumbleIntensity, ushort highFrequencyRumbleIntensity, uint durationMilliseconds);

    /// <summary>
    /// <para>
    /// Starts a rumble effect in the game controller's triggers. Each call to this function cancels any previous
    /// trigger rumble effect, and calling it with 0 intensity stops any rumbling.
    /// </para>
    /// <para>
    /// Note that this is rumbling of the <i>triggers</i> and not the game controller as a whole. This is currently
    /// only supported on Xbox One and Series controllers.
    /// </para>
    /// </summary>
    /// <param name="gameController">The controller to rumble.</param>
    /// <param name="leftRumble">The intensity of the left trigger rumble motor.</param>
    /// <param name="rightRumble">The intensity of the right trigger rumble motor.</param>
    /// <param name="durationMilliseconds">The duration of the rumble effect, in milliseconds.</param>
    /// <returns>0, or -1 if trigger rumble isn't supported on this controller.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerRumbleTriggers", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int GameControllerRumbleTriggers(SdlGameController gameController, ushort leftRumble, ushort rightRumble, uint durationMilliseconds);
    
    /// <summary>
    /// Updates a game controller's LED color.
    /// </summary>
    /// <param name="gameController">The controller to update.</param>
    /// <param name="red">The intensity of the red LED.</param>
    /// <param name="green">The intensity of the green LED.</param>
    /// <param name="blue">The intensity of the blue LED.</param>
    /// <returns>0, or -1 if this controller does not have a modifiable LED.</returns>
    /// <remarks>Added in SDL 2.0.14.</remarks>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GameControllerSetLED", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int GameControllerSetLed(SdlGameController gameController, byte red, byte green, byte blue);
}
