namespace Spectere.SdlKit.Interop.Sdl.Support.Video;

/// <summary>
/// Event subtype for display events.
/// </summary>
public enum DisplayEventId {
    /// <summary>Never used.</summary>
    None,
    
    /// <summary>Display orientation has changed do <c>data1</c>.</summary>
    Orientation,
    
    /// <summary>Display has been added to the system.</summary>
    Connected,
    
    /// <summary>Display has been removed from the system.</summary>
    Disconnected,
    
    /// <summary>Display has changed position.</summary>
    Moved
}
