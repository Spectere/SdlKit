using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Joystick axis motion event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct JoyAxisEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint Which;
    internal byte Axis;
    internal byte Padding1;
    internal byte Padding2;
    internal byte Padding3;
    internal short Value;
    internal ushort Padding4;
}
