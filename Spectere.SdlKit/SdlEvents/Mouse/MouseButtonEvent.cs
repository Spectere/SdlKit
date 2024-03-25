using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.Mouse;

/// <summary>
/// Mouse button event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct MouseButtonEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public uint WindowId;
    public uint Which;
    public MouseButton Button;
    public ButtonState State;
    public byte Clicks;
    public byte Padding1;
    public int X;
    public int Y;
}
