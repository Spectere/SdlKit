namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// The state of an event.
/// </summary>
public enum EventState {
    /// <summary>
    /// Returns the current processing state of the specified event.
    /// </summary>
    Query = -1,
    
    /// <summary>
    /// The event will automatically be dropped from the event queue and will not be filtered.
    /// </summary>
    Disable = 0,
    
    /// <summary>
    /// The event will by processed normally.
    /// </summary>
    Enable = 1
}
