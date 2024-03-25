using Spectere.SdlKit.SdlEvents;
using Spectere.SdlKit.SdlEvents.Mouse;

namespace Spectere.SdlKit.EventArgs.Mouse;

/// <summary>
/// Represents the event arguments for mouse motion events.
/// </summary>
public class MouseMotionEventArgs : CommonEventArgs<MouseMotionEvent> {
    /// <summary>
    /// Indicates whether or not a mouse button is pressed.
    /// </summary>
    public ButtonState ButtonState => SdlEvent.State;

    /// <summary>
    /// The relative motion along the X coordinate.
    /// </summary>
    public int RelativeX => SdlEvent.X;
    
    /// <summary>
    /// The relative motion along the Y coordinate.
    /// </summary>
    public int RelativeY => SdlEvent.Y;
    
    /// <summary>
    /// The X coordinate of the mouse pointer, relative to the window.
    /// </summary>
    public int PointerX => SdlEvent.X;
    
    /// <summary>
    /// The Y coordinate of the mouse pointer, relative to the window.
    /// </summary>
    public int PointerY => SdlEvent.Y;
}
