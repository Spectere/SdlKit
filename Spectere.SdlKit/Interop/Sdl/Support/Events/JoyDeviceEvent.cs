using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Joystick device event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct JoyDeviceEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint Which;
}
