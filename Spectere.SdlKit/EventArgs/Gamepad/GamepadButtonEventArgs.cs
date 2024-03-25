using Spectere.SdlKit.SdlEvents;
using Spectere.SdlKit.SdlEvents.GameController;

namespace Spectere.SdlKit.EventArgs.Gamepad;

/// <summary>
/// Represents the event arguments for gamepad button events.
/// </summary>
public class GamepadButtonEventArgs : CommonEventArgs<GameControllerButtonEvent> {
    /// <summary>
    /// The button that is being represented by this event.
    /// </summary>
    public GameControllerButton Button => SdlEvent.Button;
    
    /// <summary>
    /// The identifier of the device that generated this event.
    /// </summary>
    public int DeviceId => SdlEvent.Which;

    /// <summary>
    /// A <see cref="ButtonState"/> indicating the status of this button.
    /// </summary>
    public ButtonState State => SdlEvent.State;
}
