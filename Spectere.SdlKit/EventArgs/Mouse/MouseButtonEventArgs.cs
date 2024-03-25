using Spectere.SdlKit.SdlEvents;
using Spectere.SdlKit.SdlEvents.Mouse;

namespace Spectere.SdlKit.EventArgs.Mouse;

/// <summary>
/// Represents the event arguments for mouse button released events.
/// </summary>
public class MouseButtonEventArgs : CommonEventArgs<MouseButtonEvent> {
    /// <summary>
    /// The mouse button that this event is referencing.
    /// </summary>
    public MouseButton Button => SdlEvent.Button;

    /// <summary>
    /// The number of clicks that occurred.
    /// </summary>
    public byte Clicks => SdlEvent.Clicks;

    /// <summary>
    /// The X coordinate of the mouse pointer, relative to the window.
    /// </summary>
    public int PointerX => SdlEvent.X;

    /// <summary>
    /// The Y coordinate of the mouse pointer, relative to the window.
    /// </summary>
    public int PointerY => SdlEvent.Y;

    /// <summary>
    /// A <see cref="ButtonState"/> indicating the status of this button.
    /// </summary>
    public ButtonState State => SdlEvent.State;
}
