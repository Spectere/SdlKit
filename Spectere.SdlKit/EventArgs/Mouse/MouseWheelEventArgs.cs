using Spectere.SdlKit.SdlEvents.Mouse;

namespace Spectere.SdlKit.EventArgs.Mouse;

/// <summary>
/// Represents the event arguments for mouse wheel events.
/// </summary>
public class MouseWheelEventArgs : CommonEventArgs<MouseWheelEvent> {
    /// <summary>
    /// The amount that the horizontal scroll wheel has been moved, represented as an integer. Typically, positive
    /// values indicate movement to the right, while negative values represent left movement. If
    /// <see cref="NaturalScrolling"/> is <c>true</c>, this will be reversed.
    /// </summary>
    public int HorizontalDelta => SdlEvent.X;

    /// <summary>
    /// The amount that the horizontal scroll wheel has been moved, represented as a floating point value. Typically,
    /// positive values indicate movement to the right, while negative values represent left movement. If
    /// <see cref="NaturalScrolling"/> is <c>true</c>, this will be reversed.
    /// </summary>
    /// <remarks>Added in SDL 2.0.18.</remarks>
    public float HorizontalDeltaPrecise => SdlEvent.PreciseX;

    /// <summary>
    /// If this is set to <c>true</c>, the user is using natural, or reversed, scrolling. This is most common with
    /// trackpads.
    /// </summary>
    public bool NaturalScrolling => SdlEvent.Direction == MouseWheelDirection.Flipped;

    /// <summary>
    /// The X position of the mouse pointer, relative to the window.
    /// </summary>
    /// <remarks>Added in SDL 2.26.0.</remarks>
    public int PointerX => SdlEvent.MouseX;
    
    /// <summary>
    /// The Y position of the mouse pointer, relative to the window.
    /// </summary>
    /// <remarks>Added in SDL 2.26.0.</remarks>
    public int PointerY => SdlEvent.MouseY;
    
    /// <summary>
    /// The amount that the vertical scroll wheel has been moved, represented as an integer. Typically, positive values
    /// indicate that the scroll wheel has been moved up (away from the user), while negative values indicate that the
    /// scroll wheel has been moved down (toward the user). If <see cref="NaturalScrolling"/> is <c>true</c>, this will
    /// be reversed.
    /// </summary>
    public int VerticalDelta => SdlEvent.Y;

    /// <summary>
    /// The amount that the vertical scroll wheel has been moved, represented as a floating point value. Typically,
    /// positive values indicate that the scroll wheel has been moved up (away from the user), while negative values
    /// indicate that the scroll wheel has been moved down (toward the user). If <see cref="NaturalScrolling"/> is
    /// <c>true</c>, this will be reversed.
    /// </summary>
    /// <remarks>Added in SDL 2.0.18.</remarks>
    public float VerticalDeltaPrecise => SdlEvent.PreciseY;
}
