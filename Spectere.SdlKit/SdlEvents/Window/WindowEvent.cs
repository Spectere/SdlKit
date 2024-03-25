using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.Window;

/// <summary>
/// Window state change event data.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct WindowEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public uint WindowId;
    public WindowEventType EventId;
    public byte Padding1;
    public byte Padding2;
    public byte Padding3;
    public int Data1;
    public int Data2;
}
