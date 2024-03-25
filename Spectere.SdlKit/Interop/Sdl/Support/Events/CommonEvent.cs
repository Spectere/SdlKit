using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Fields shared by every event.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct CommonEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
}
