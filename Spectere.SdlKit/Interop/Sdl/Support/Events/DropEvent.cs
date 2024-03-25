using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// A event used to request a file open by the system. This event is enabled by default, you can disable it
/// with SDL_EventState().
/// </summary>
/// <remarks>
/// If this event is enabled, you must free the filename in the event.
/// </remarks>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal unsafe struct DropEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal byte* File;
    internal uint WindowId;
}
