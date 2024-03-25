namespace Spectere.SdlKit.EventArgs.Gamepad;

/// <summary>
/// Represents a gamepad touchpad action.
/// </summary>
public enum GamepadTouchpadEventType {
    /// <summary>
    /// An unrecognized or invalid event occurred.
    /// </summary>
    Unknown,
    
    /// <summary>
    /// Indicate that a user moved their finger along the touchpad's surface.
    /// </summary>
    GamepadTouchpadMotion,

    /// <summary>
    /// Indicates that the user placed a finger on the touchpad.
    /// </summary>
    GamepadTouchpadDown,
    
    /// <summary>
    /// Indicates that a user lifted their finger off the touchpad.
    /// </summary>
    GamepadTouchpadUp
}
