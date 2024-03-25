using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// A video driver dependent system event. This event is disabled by default, you can enable it with
/// SDL_EventState().
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct SysWmEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal IntPtr Msg;
}
