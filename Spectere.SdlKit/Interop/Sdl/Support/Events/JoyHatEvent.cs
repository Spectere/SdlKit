using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Joystick hat position change event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct JoyHatEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint Which;
    internal byte Hat;
    internal JoystickHatPosition Value;
    internal byte Padding1;
    internal byte Padding2;
}
