using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Joystick trackball motion event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct JoyBallEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint Which;
    internal byte Ball;
    internal byte Padding1;
    internal byte Padding2;
    internal byte Padding3;
    internal short XRel;
    internal short YRel;
}
