namespace Spectere.SdlKit.SdlEvents; 

/// <summary>
/// Indicates the state of a keyboard, mouse, joystick, or controller button.
/// </summary>
public enum ButtonState : byte {
    /// <summary>Button is not currently pressed.</summary>
    Released = 0,
    
    /// <summary>Button is currently being pressed.</summary>
    Pressed = 1
}
