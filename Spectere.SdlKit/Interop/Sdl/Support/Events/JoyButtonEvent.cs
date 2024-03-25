using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Joystick button event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct JoyButtonEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint Which;
    internal byte Button;
    internal ButtonState State;
    internal byte Padding1;
    internal byte Padding2;
}
