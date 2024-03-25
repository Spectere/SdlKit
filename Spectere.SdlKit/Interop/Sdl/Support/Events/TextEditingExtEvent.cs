using Spectere.SdlKit.SdlEvents;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// Extended keyboard text editing event structure when text would be truncated if stored in the text
/// buffer <see cref="TextEditingEvent"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
internal unsafe struct TextEditingExtEvent {
    internal SdlEventType Type;
    internal uint Timestamp;
    internal uint WindowId;
    internal byte* Text;
    internal int Start;
    internal int Length;
}
