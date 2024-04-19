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
    /// If <c>true</c>, this key event was triggered by the operating system's auto-repeat functionality. If this is
    /// <c>false</c>, this indicates the first time the user has pressed the key. This only impacts key down events
    /// and will always return <c>false</c> for key up.
    /// </summary>
    public bool Repeat => SdlEvent.Repeat > 0;

    /// <summary>
    /// A <see cref="ButtonState"/> indicating the status of this button.
    /// </summary>
    public ButtonState State => SdlEvent.State;
}
