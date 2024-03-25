namespace Spectere.SdlKit.SdlEvents.Mouse; 

/// <summary>
/// The set of mouse buttons supported by SDL2.
/// </summary>
public enum MouseButton : byte {
    /// <summary>The left mouse button.</summary>
    Left = 1,
    
    /// <summary>The middle mouse button.</summary>
    Middle = 2,
    
    /// <summary>The right mouse button.</summary>
    Right = 3,
    
    /// <summary>The first side button (back).</summary>
    X1 = 4,
    
    /// <summary>The second site button (forward).</summary>
    X2 = 5
}
