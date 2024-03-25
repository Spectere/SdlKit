using Spectere.SdlKit.SdlEvents.Window;

namespace Spectere.SdlKit.EventArgs.Window;

/// <summary>
/// Represents the event arguments for a window movement event.
/// </summary>
public class WindowMovedEventArgs : CommonEventArgs<WindowEvent> {
    /// <summary>
    /// The new X coordinate of the window's upper-left corner.
    /// </summary>
    public int X => SdlEvent.Data1;

    /// <summary>
    /// The new Y coordinate of the window's upper-left corner.
    /// </summary>
    public int Y => SdlEvent.Data2;
}
