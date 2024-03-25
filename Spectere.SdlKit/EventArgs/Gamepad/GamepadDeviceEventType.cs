namespace Spectere.SdlKit.EventArgs.Gamepad;

/// <summary>
/// Represents a gamepad hardware event.
/// </summary>
public enum GamepadDeviceEventType {
    /// <summary>
    /// An unrecognized or invalid event occurred.
    /// </summary>
    Unknown,
    
    /// <summary>
    /// Indicates that a gamepad has been plugged into the system.
    /// </summary>
    GamepadDeviceAdded,
    
    /// <summary>
    /// Indicates that a gamepad has been unplugged from the system.
    /// </summary>
    GamepadDeviceRemoved,
    
    /// <summary>
    /// Indicates that a gamepad's inputs have been remapped.
    /// </summary>
    GamepadDeviceRemapped
}
