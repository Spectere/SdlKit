using Spectere.SdlKit.SdlEvents.GameController;

namespace Spectere.SdlKit.EventArgs.Gamepad;

/// <summary>
/// Represents the event arguments for gamepad axis events.
/// </summary>
public class GamepadAxisEventArgs : CommonEventArgs<GameControllerAxisEvent> {
    /// <summary>
    /// The axis represented by this event.
    /// </summary>
    public GameControllerAxis Axis => SdlEvent.Axis;

    /// <summary>
    /// The identifier of the device that generated this event.
    /// </summary>
    public int DeviceId => SdlEvent.Which;

    /// <summary>
    /// The value of the axis. For X axis events, negative and positive values represent left and right, respectively.
    /// For Y axis events, negative and positive values are up and down. For triggers, <see cref="short.MinValue"/>
    /// represents a trigger that's completely released, while <see cref="short.MaxValue"/> is used for a trigger
    /// that's completely pressed in.
    /// </summary>
    public short Position => SdlEvent.Value;
}
