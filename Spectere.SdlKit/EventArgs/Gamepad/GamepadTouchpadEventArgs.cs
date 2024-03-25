using Spectere.SdlKit.SdlEvents;
using Spectere.SdlKit.SdlEvents.GameController;

namespace Spectere.SdlKit.EventArgs.Gamepad;

/// <summary>
/// Represents the event arguments for gamepad touchpad finger events.
/// </summary>
public class GamepadTouchpadEventArgs : CommonEventArgs<GameControllerTouchpadEvent> {
    /// <summary>
    /// The identifier of the device that generated this event.
    /// </summary>
    public int DeviceId => SdlEvent.Which;

    /// <summary>
    /// The event type that occurred during this event instance.
    /// </summary>
    public GamepadTouchpadEventType EventType => SdlEvent.Type switch {
        SdlEventType.GameControllerTouchpadDown => GamepadTouchpadEventType.GamepadTouchpadDown,
        SdlEventType.GameControllerTouchpadMotion => GamepadTouchpadEventType.GamepadTouchpadMotion,
        SdlEventType.GameControllerTouchpadUp => GamepadTouchpadEventType.GamepadTouchpadUp,
        _ => GamepadTouchpadEventType.Unknown
    };
    
    /// <summary>
    /// The index of the finger on the touchpad.
    /// </summary>
    public int Finger => SdlEvent.Finger;

    /// <summary>
    /// The amount of pressure the user's finger is putting on the touchpad, ranging from 0.0 to 1.0.
    /// </summary>
    public float Pressure => SdlEvent.Pressure;
    
    /// <summary>
    /// The X coordinate where the finger is located, ranging from 0.0 to 1.0, with 0.0 being on the left.
    /// </summary>
    public float X => SdlEvent.X;

    /// <summary>
    /// The Y coordinate where the finger is located, ranging from 0.0 to 1.0, with 0.0 being at the top.
    /// </summary>
    public float Y => SdlEvent.Y;
}
