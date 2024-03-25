namespace Spectere.SdlKit.SdlEvents.Mouse; 

/// <summary>
/// A mask used to indicate multiple buttons being pressed simultaneously.
/// </summary>
[Flags]
internal enum MouseButtonMask {
    /// <summary>The left mouse button.</summary>
    Left = 0x0001,

    /// <summary>The middle mouse button.</summary>
    Middle = 0x0002,
    
    /// <summary>The right mouse button.</summary>
    Right = 0x0004,
    
    /// <summary>The first side button (back).</summary>
    X1 = 0x0008,
    
    /// <summary>The second site button (forward).</summary>
    X2 = 0x0010
}
