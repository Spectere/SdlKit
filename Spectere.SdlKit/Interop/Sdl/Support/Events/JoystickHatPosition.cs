namespace Spectere.SdlKit.Interop.Sdl.Support.Events; 

/// <summary>
/// A list of possible positions that the joystick hat can be in.
/// </summary>
[Flags]
internal enum JoystickHatPosition : byte {
    /// <summary>The joystick hat is in the center (neutral) position.</summary>
    Centered = 0x00,
    
    /// <summary>The joystick hat is in the up position.</summary>
    Up = 0x01,
    
    /// <summary>The joystick hat is in the right position.</summary>
    Right = 0x02,
    
    /// <summary>The joystick hat is in the down position.</summary>
    Down = 0x04,
    
    /// <summary>The joystick hat is in the left position.</summary>
    Left = 0x08,

    /// <summary>The joystick hat is in the upper-right position.</summary>
    RightUp = Up | Right,
    
    /// <summary>The joystick hat is in the lower-right position.</summary>
    RightDown = Down | Right,
    
    /// <summary>The joystick hat is in the upper-left position.</summary>
    LeftUp = Up | Left,
    
    /// <summary>The joystick hat is in the lower-left position.</summary>
    LeftDown = Down | Left
}
