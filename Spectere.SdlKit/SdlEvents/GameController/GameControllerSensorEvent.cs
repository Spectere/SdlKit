using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.GameController;

/// <summary>
/// Game controller sensor event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct GameControllerSensorEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public int Which;
    public SensorType Sensor;
    public float Data1;
    public float Data2;
    public float Data3;
    public ulong TimestampMicroseconds;
}
