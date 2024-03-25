namespace Spectere.SdlKit.EventArgs;

/// <summary>
/// Contains data that is common among all event argument classes.
/// </summary>
/// <typeparam name="T">The type of SDL event data represented in these event arguments.</typeparam>
public class CommonEventArgs<T> {
    /// <summary>
    /// The raw event data passed in from SDL.
    /// </summary>
    public required T SdlEvent { get; init; }
}
