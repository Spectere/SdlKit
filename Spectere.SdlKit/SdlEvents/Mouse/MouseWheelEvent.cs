using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.Mouse;

/// <summary>
/// Mouse wheel event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct MouseWheelEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public uint WindowId;
    public uint Which;
    public int X;
    public int Y;
    public MouseWheelDirection Direction;
    public float PreciseX;
    public float PreciseY;
    public int MouseX;
    public int MouseY;
}
