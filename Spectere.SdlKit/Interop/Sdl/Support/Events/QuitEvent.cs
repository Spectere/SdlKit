using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// The "quit requested" event.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct QuitEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
}
