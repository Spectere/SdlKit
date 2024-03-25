namespace Spectere.SdlKit.Interop.Sdl.Support.Video;

/// <summary>
/// Possible returns from the HitTest callback.
/// </summary>
public enum HitTestResult {
    /// <summary>
    /// Region is normal. No special properties.
    /// </summary>
    Normal,
    
    /// <summary>
    /// Region can drag entire window.
    /// </summary>
    Draggable,
    
    ResizeTopLeft,
    ResizeTop,
    ResizeTopRight,
    ResizeRight,
    ResizeBottomRight,
    ResizeBottom,
    ResizeBottomLeft,
    ResizeLeft
}
