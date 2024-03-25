using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Dollar gesture event.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct DollarGestureEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal long TouchId;
    internal long GestureId;
    internal uint NumFingers;
    internal float Error;
    internal float X;
    internal float Y;
}
