using Spectere.SdlKit.EventArgs.Gamepad;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.GameController;
using Spectere.SdlKit.Interop.Sdl.Support.Init;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit;

/// <summary>
/// Describes a gamepad that was plugged into the system.
/// </summary>
public class Gamepad {
    #region Static Methods/Properties
    /// <summary>
    /// Gets the status of the gamepad subsystem. If you intend to enable or disable gamepad support, use the
    /// <see cref="EnableSubsystem"/> and <see cref="DisableSubsystem"/> methods.
    /// </summary>
    public static bool Enabled { get; private set; }
    
    /// <summary>
    /// Disables the gamepad subsystem. If this is called, gamepad-related events will no longer be called. Note that
    /// you do not have to explicitly call this when your application is exiting. If the gamepad subsystem is enabled
    /// during shutdown, SdlKit will shut it down automatically.
    /// </summary>
    /// <returns><c>true</c> if the gamepad subsystem was shut down successfully, otherwise <c>false</c>.</returns>
    public static bool DisableSubsystem() {
        if(!Enabled) {
            return true;
        }
        
        var result = Init.QuitSubSystem(SubsystemFlags.GameController) == 0;
        Enabled = !result;
        return result;
    }

    /// <summary>
    /// Enables the gamepad subsystem. This must be called for gamepad-related events to be called.
    /// </summary>
    /// <returns><c>true</c> if the gamepad subsystem was initialized successfully, otherwise <c>false</c>.</returns>
    public static bool EnableSubsystem() {
        if(Enabled) {
            // SDL subsystems are reference counted, so we definitely don't want to mistakenly initialize this multiple
            // times.
            return true;
        }

        var result = Init.InitSubSystem(SubsystemFlags.GameController) == 0;
        Enabled = result;
        return result;
    }
    
    /// <summary>
    /// Opens a gamepad. This should be performed when a <see cref="GamepadDeviceEventType.GamepadDeviceAdded"/> is
    /// encountered, and must be done before the gamepad can be used.
    /// </summary>
    /// <param name="deviceId">The device ID of the gamepad. This can be found from the <see cref="GamepadDeviceEventArgs"/>
    /// object passed in by the <see cref="Window.OnGamepadDeviceAdded"/> event.</param>
    /// <returns>A <see cref="Gamepad"/> class representing the opened gamepad, containing information about the
    /// gamepad. An instance of this will be needed to close the gamepad later, either on device removal or during the
    /// exit process. If this is <c>null</c>, an error occurred while opening the gamepad.</returns>
    public static Gamepad? Open(int deviceId) {
        var sdlGameController = GameController.GameControllerOpen(deviceId);
        return sdlGameController.IsNull ? null : new Gamepad(deviceId, sdlGameController);
    }
    #endregion
    
    /// <summary>
    /// The device ID of this <see cref="Gamepad"/>. This is commonly used by SDL to reference a particular controller.
    /// </summary>
    public int DeviceId { get; }
    
    /// <summary>
    /// The name of this controller.
    /// </summary>
    public string? Name { get; }
    
    /// <summary>
    /// The player index of this controller.
    /// </summary>
    /// <remarks>Added in SDL 2.0.12.</remarks>
    public int PlayerIndex { get; }
    
    /// <summary>
    /// The SDL game controller instance representing this gamepad. This is only useful inside of SdlKit.
    /// </summary>
    private readonly SdlGameController _sdlGameController;
    
    /// <summary>
    /// Gets whether or not the controller has a modifiable LED.
    /// </summary>
    public bool SupportsLedColorChange { get; }
    
    /// <summary>
    /// Gets whether or not the controller supports rumble.
    /// </summary>
    public bool SupportsRumble { get; }
    
    /// <summary>
    /// Gets whether or not the controller supports rumble triggers.
    /// </summary>
    public bool SupportsRumbleTriggers { get; }
    
    /// <summary>
    /// Gets the number of touchpads this controller has.
    /// </summary>
    public int TouchpadCount { get; }
    
    /// <summary>
    /// Gets the type of controller.
    /// </summary>
    public GamepadType Type { get; }
    
    /// <summary>
    /// Initializes a new <see cref="Gamepad"/> class instance. This can only be performed by SdlKit.
    /// </summary>
    /// <param name="sdlGameController">The SDL game controller object to base this object on.</param>
    internal Gamepad(int deviceId, SdlGameController sdlGameController) {
        DeviceId = deviceId;
        _sdlGameController = sdlGameController;
        
        // Get general controller information.
        Name = Marshal.PtrToStringAnsi(GameController.GameControllerName(_sdlGameController));
        PlayerIndex = GameController.GameControllerGetPlayerIndex(_sdlGameController);
        Type = GameController.GameControllerGetType(_sdlGameController);

        // Get supported features.
        SupportsLedColorChange = GameController.GameControllerHasLed(_sdlGameController);
        SupportsRumble = GameController.GameControllerHasRumble(_sdlGameController);
        SupportsRumbleTriggers = GameController.GameControllerHasRumbleTriggers(_sdlGameController);
        TouchpadCount = GameController.GameControllerGetNumTouchpads(_sdlGameController);
    }

    /// <summary>
    /// Closes this <see cref="Gamepad"/>.
    /// </summary>
    public void Close() {
        if(_sdlGameController.IsNull) {
            return;
        }
        GameController.GameControllerClose(_sdlGameController);
    }

    /// <summary>
    /// Starts a rumble effect on this controller. Each call to this method will cancel any previous rumble effect,
    /// and calling it with 0 intensity stops and rumbling on that motor.
    /// </summary>
    /// <param name="lowFrequencyRumble">The intensity of the low frequency (left) rumble motor, from
    /// <see cref="ushort.MinValue"/> to <see cref="ushort.MaxValue"/>.</param>
    /// <param name="highFrequencyRumble">The intensity of the high frequency (right) rumble motor, from
    /// <see cref="ushort.MinValue"/> to <see cref="ushort.MaxValue"/>.</param>
    /// <param name="durationMilliseconds">The duration of the rumble effect, in milliseconds.</param>
    /// <returns><c>true</c> if the rumble effect was started successfully, or <c>false</c> if it is not supported by
    /// the controller.</returns>
    /// <remarks>Added in SDL 2.0.9.</remarks>
    public bool Rumble(ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMilliseconds) =>
        GameController.GameControllerRumble(_sdlGameController, lowFrequencyRumble, highFrequencyRumble, durationMilliseconds) == 0;

    /// <summary>
    /// <para>
    /// Starts a trigger rumble effect on this controller. Each call to this method will cancel any previous trigger
    /// rumble effect, and calling it with 0 intensity stops and rumbling on that motor.
    /// </para>
    /// <para>
    /// This rumble effect only affects the controller's triggers and is only supported by a few controllers on the
    /// market (such as the Xbox One/Series controllers). For the more common type of rumble, use the <see cref="Rumble"/>
    /// method instead.
    /// </para>
    /// </summary>
    /// <param name="lowFrequencyRumble">The intensity of the low frequency (left) rumble motor, from
    /// <see cref="ushort.MinValue"/> to <see cref="ushort.MaxValue"/>.</param>
    /// <param name="highFrequencyRumble">The intensity of the high frequency (right) rumble motor, from
    /// <see cref="ushort.MinValue"/> to <see cref="ushort.MaxValue"/>.</param>
    /// <param name="durationMilliseconds">The duration of the rumble effect, in milliseconds.</param>
    /// <returns><c>true</c> if the rumble effect was started successfully, or <c>false</c> if it is not supported by
    /// the controller.</returns>
    /// <remarks>Added in SDL 2.0.14.</remarks>
    public bool RumbleTriggers(ushort lowFrequencyRumble, ushort highFrequencyRumble, uint durationMilliseconds) =>
        GameController.GameControllerRumbleTriggers(_sdlGameController, lowFrequencyRumble, highFrequencyRumble, durationMilliseconds) == 0;

    /// <summary>
    /// Sets the color of this gamepad's LED, if it has one.
    /// </summary>
    /// <param name="gamepad">The <see cref="Gamepad"/> to affect.</param>
    /// <param name="color">An <see cref="SdlColor"/> object describing which colors to change the gamepad LED to.
    /// The alpha channel will be ignored.</param>
    /// <returns><c>true</c> if the LED was successfully configured; <c>false</c> if the gamepad instance is not valid
    /// or the gamepad does not have a modifiable LED.</returns>
    public bool SetLedColor(SdlColor color) => SetLedColor(color.R, color.G, color.B);

    /// <summary>
    /// Sets the color of this gamepad's LED, if it has one.
    /// </summary>
    /// <param name="gamepad">The <see cref="Gamepad"/> to affect.</param>
    /// <param name="red">The red component of the LED.</param>
    /// <param name="green">The green component of the LED.</param>
    /// <param name="blue">The blue component of the LED.</param>
    /// <returns><c>true</c> if the LED was successfully configured; <c>false</c> if the gamepad instance is not valid
    /// or the gamepad does not have a modifiable LED.</returns>
    public bool SetLedColor(byte red, byte green, byte blue) {
        if(_sdlGameController.IsNull) {
            return false;
        }
        
        return GameController.GameControllerSetLed(_sdlGameController, red, green, blue) == 0;
    }
}
