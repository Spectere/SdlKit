using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// A user-defined event type.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal struct UserEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint WindowId;
    internal int Code;
    internal IntPtr Data1;
    internal IntPtr Data2;
}
