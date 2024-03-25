namespace Spectere.SdlKit.Interop.Sdl.Support.Video;

/// <summary>
/// Display orientation.
/// </summary>
public enum DisplayOrientation {
    /// <summary>The display orientation can't be determined.</summary>
    Unknown,
    
    /// <summary>The display is in landscape mode, with the right side up, relative to portrait mode.</summary>
    Landscape,
    
    /// <summary>The display is in landscape mode, with the left side up, relative to portrait mode.</summary>
    LandscapeFlipped,
    
    /// <summary>The display is in portrait mode.</summary>
    Portrait,
    
    /// <summary>The display is in portrait mode, upside down.</summary>
    PortraitFlipped
}
