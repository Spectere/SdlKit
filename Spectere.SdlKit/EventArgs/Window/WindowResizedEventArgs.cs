using Spectere.SdlKit.SdlEvents.Window;

namespace Spectere.SdlKit.EventArgs.Window;

/// <summary>
/// Represents the event arguments for when a window is resized by the user.
/// </summary>
public class WindowResizedEventArgs : CommonEventArgs<WindowEvent> {
    /// <summary>
    /// The new width of the window.
    /// </summary>
    public int Width => SdlEvent.Data1;

    /// <summary>
    /// The new height of the window.
    /// </summary>
    public int Height => SdlEvent.Data2;
}
