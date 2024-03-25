using Spectere.SdlKit.SdlEvents;
using Spectere.SdlKit.SdlEvents.Keyboard;

namespace Spectere.SdlKit.EventArgs.Keyboard;

/// <summary>
/// Represents the event arguments for key pressed events.
/// </summary>
public class KeyEventArgs : CommonEventArgs<KeyboardEvent> {
    /// <summary>
    /// The key that the user has pressed.
    /// </summary>
    public Keycode KeyCode => SdlEvent.Keysym.Sym;

    /// <summary>
    /// The current modifier keys.
    /// </summary>
    public KeyModifier Modifier => SdlEvent.Keysym.Mod;

    /// <summary>
    /// A <see cref="ButtonState"/> indicating the status of this button.
    /// </summary>
    public ButtonState State => SdlEvent.State;
}
