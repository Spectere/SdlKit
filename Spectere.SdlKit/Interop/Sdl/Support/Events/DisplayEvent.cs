using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Display state change event data.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct DisplayEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint Display;
    internal byte Event;  // SDL_DisplayEventID
    internal byte Padding1;
    internal byte Padding2;
    internal byte Padding3;
    internal int Data1;
}
