using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Audio device event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct AudioDeviceEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint Which;
    internal byte IsCapture;
    internal byte Padding1;
    internal byte Padding2;
    internal byte Padding3;
}
