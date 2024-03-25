namespace Spectere.SdlKit.Interop.Sdl.Support.Video;

/// <summary>
/// Event subtype for window events.
/// </summary>
public enum WindowEventId {
    /// <summary>Never used.</summary>
    None,
    
    /// <summary>Window has been shown.</summary>
    Shown,
    
    /// <summary>Window has been hidden.</summary>
    Hidden,
    
    /// <summary>Window has been exposed and should be redrawn.</summary>
    Exposed,
    
    /// <summary>Window has been moved to <c>data1</c>, <c>data2</c>.</summary>
    Moved,
    
    /// <summary>Window has been resized to <c>data1</c> x <c>data2</c>.</summary>
    Resized,
    
    /// <summary>The window size has been changed, either as a result of an API call or through the system or user
    /// changing the window size.</summary>
    SizeChanged,
    
    /// <summary>Window has been minimized.</summary>
    Minimized,
    
    /// <summary>Window has been maximized.</summary>
    Maximized,
    
    /// <summary>Window has been restored to normal size and position.</summary>
    Restored,
    
    /// <summary>Window has gained mouse focus.</summary>
    Enter,
    
    /// <summary>Window has lost mouse focus.</summary>
    Leave,
    
    /// <summary>Window has gained keyboard focus.</summary>
    FocusGained,
    
    /// <summary>Window has lost keyboard focus.</summary>
    FocusLost,
    
    /// <summary>The window manager requests that the window be closed.</summary>
    Close,
    
    /// <summary>Window is being offered a focus (it should call SetWindowInputFocus() on itself or a subwindow, or ignore).</summary>
    TakeFocus,
    
    /// <summary>Window had a hit test that wasn't SDL_HITTEST_NORMAL.</summary>
    HitTest,
    
    /// <summary>The ICC profile of the window's display has changed.</summary>
    IccProfChanged,
    
    /// <summary>Window has been moved to display <c>data1</c>.</summary>
    DisplayChanged
}
