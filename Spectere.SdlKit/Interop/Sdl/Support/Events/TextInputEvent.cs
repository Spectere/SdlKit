using Spectere.SdlKit.SdlEvents;
using EventMain = Spectere.SdlKit.Interop.Sdl.Events;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Keyboard text input event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal unsafe struct TextInputEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint WindowId;
    internal fixed byte Text[EventMain.TextEditingEventTextSize];
}
