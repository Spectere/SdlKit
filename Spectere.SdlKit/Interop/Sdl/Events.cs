using Spectere.SdlKit.Interop.Sdl.Support.Events;
using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl;

/// <summary>
/// Contains the necessary constants and function imports from SDL_events.h.
/// </summary>
internal static class Events {
    internal const int TextEditingEventTextSize = 32;
    
    /// <summary>
    /// Set the state of processing events by type.
    /// </summary>
    /// <param name="type">The type of event.</param>
    /// <param name="state">An <see cref="EventState"/> specifying whether the event state should be
    /// queries or set to a given value.</param>
    /// <returns>Either <see cref="EventState.Disable"/> or <see cref="EventState.Enable"/>, representing
    /// the state of the event before this function makes any changes to it.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_EventState", CallingConvention = CallingConvention.Cdecl)]
    internal static extern EventState EventState(SdlEventType type, EventState state);

    /// <summary>
    /// Polls for currently pending events.
    /// </summary>
    /// <param name="sdlEvent">An object to store event data into. If this is not NULL, the event is removed from the
    /// queue and stored into the object.</param>
    /// <returns>1 if there are any pending events, or 0 if there are none available.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_PollEvent", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int PollEvent(out Event sdlEvent);
}
