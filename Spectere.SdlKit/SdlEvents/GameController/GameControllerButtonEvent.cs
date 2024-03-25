using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.GameController;

/// <summary>
/// Game controller button event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct GameControllerButtonEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public int Which;
    public GameControllerButton Button;
    public ButtonState State;
    public byte Padding1;
    public byte Padding2;
}
