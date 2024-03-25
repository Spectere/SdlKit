using Spectere.SdlKit.SdlEvents;
using Spectere.SdlKit.SdlEvents.GameController;

namespace Spectere.SdlKit.EventArgs.Gamepad;

/// <summary>
/// Represents the event arguments for gamepad device added/removed events.
/// </summary>
public class GamepadDeviceEventArgs : CommonEventArgs<GameControllerDeviceEvent> {
    /// <summary>
    /// The identifier of the device that generated this event.
    /// </summary>
    public int DeviceId => SdlEvent.Which;

    /// <summary>
    /// The event type that occurred during this event instance.
    /// </summary>
    public GamepadDeviceEventType EventType => SdlEvent.Type switch {
        SdlEventType.GameControllerDeviceAdded => GamepadDeviceEventType.GamepadDeviceAdded,
        SdlEventType.GameControllerDeviceRemapped => GamepadDeviceEventType.GamepadDeviceRemapped,
        SdlEventType.GameControllerDeviceRemoved => GamepadDeviceEventType.GamepadDeviceRemoved,
        _ => GamepadDeviceEventType.Unknown
    };
}
