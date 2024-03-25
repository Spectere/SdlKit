using System.Runtime.InteropServices;

namespace Spectere.SdlKit.SdlEvents.Keyboard;

/// <summary>
/// Keyboard button event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 56)]
public struct KeyboardEvent {
    public SdlEventType Type;
    public uint Timestamp;
    public uint WindowId;
    public ButtonState State;
    public byte Repeat;
    public byte Padding2;
    public byte Padding3;
    public Keysym Keysym;
}
