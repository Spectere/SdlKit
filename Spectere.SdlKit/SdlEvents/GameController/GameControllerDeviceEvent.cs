using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.GameController;

/// <summary>
/// Controller device event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct GameControllerDeviceEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public int Which;
}
