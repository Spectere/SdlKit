using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.GameController;

/// <summary>
/// Game controller axis motion event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct GameControllerAxisEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public int Which;
    public GameControllerAxis Axis;
    public byte Padding1;
    public byte Padding2;
    public byte Padding3;
    public short Value;
    public ushort Padding4;
}
