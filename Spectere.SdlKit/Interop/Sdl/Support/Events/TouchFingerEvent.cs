using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Touch finger event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct TouchFingerEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal long TouchId;
    internal long FingerId;
    internal float X;
    internal float Y;
    internal float DX;
    internal float DY;
    internal float Pressure;
}
