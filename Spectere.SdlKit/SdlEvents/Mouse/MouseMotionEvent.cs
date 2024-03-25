using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.Mouse;

/// <summary>
/// Mouse motion event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct MouseMotionEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public uint WindowId;
    public uint Which;
    public ButtonState State;
    public int X;
    public int Y;
    public int XRel;
    public int YRel;
}
