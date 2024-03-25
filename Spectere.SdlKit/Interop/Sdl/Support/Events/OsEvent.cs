using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// OS-specific event.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct SdlOsEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
}
