using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.GameController;

/// <summary>
/// Game controller touchpad event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct GameControllerTouchpadEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public int Which;
    public int Touchpad;
    public int Finger;
    public float X;
    public float Y;
    public float Pressure;
}
