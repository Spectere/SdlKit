using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Multiple finger gesture event.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct MultiGestureEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal long TouchId;
    internal float DTheta;
    internal float DDist;
    internal float X;
    internal float Y;
    internal ushort NumFingers;
    internal ushort Padding;
}
