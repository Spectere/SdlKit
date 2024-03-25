using Spectere.SdlKit.SdlEvents;
using Spectere.SdlKit.SdlEvents.GameController;
using Spectere.SdlKit.SdlEvents.Keyboard;
using Spectere.SdlKit.SdlEvents.Mouse;
using Spectere.SdlKit.SdlEvents.Window;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Events;

/// <summary>
/// General event structure.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 56)]
internal unsafe struct Event {
    [FieldOffset(0)] public SdlEventType Type;
    [FieldOffset(4)] private uint Timestamp;
    [FieldOffset(8)] private fixed byte Padding[48];

    [FieldOffset(0)] public CommonEvent Common;
    [FieldOffset(0)] public DisplayEvent Display;
    [FieldOffset(0)] public WindowEvent Window;
    [FieldOffset(0)] public KeyboardEvent Keyboard;
    [FieldOffset(0)] public TextEditingEvent TextEditing;
    [FieldOffset(0)] public TextEditingExtEvent TextEditingExt;
    [FieldOffset(0)] public TextInputEvent TextInput;
    [FieldOffset(0)] public MouseMotionEvent MouseMotion;
    [FieldOffset(0)] public MouseButtonEvent MouseButton;
    [FieldOffset(0)] public MouseWheelEvent MouseWheel;
    [FieldOffset(0)] public JoyAxisEvent JoyAxis;
    [FieldOffset(0)] public JoyBallEvent JoyBall;
    [FieldOffset(0)] public JoyHatEvent JoyHat;
    [FieldOffset(0)] public JoyButtonEvent JoyButton;
    [FieldOffset(0)] public JoyDeviceEvent JoyDevice;
    [FieldOffset(0)] public GameControllerAxisEvent GameControllerAxis;
    [FieldOffset(0)] public GameControllerButtonEvent GameControllerButton;
    [FieldOffset(0)] public GameControllerDeviceEvent GameControllerDevice;
    [FieldOffset(0)] public GameControllerTouchpadEvent GameControllerTouchpad;
    [FieldOffset(0)] public GameControllerSensorEvent GameControllerSensor;
    [FieldOffset(0)] public AudioDeviceEvent AudioDevice;
    [FieldOffset(0)] public QuitEvent Quit;
    [FieldOffset(0)] public UserEvent User;
    [FieldOffset(0)] public SysWmEvent SysWm;
    [FieldOffset(0)] public TouchFingerEvent TouchFinger;
    [FieldOffset(0)] public MultiGestureEvent MultiGesture;
    [FieldOffset(0)] public DollarGestureEvent DollarGesture;
    [FieldOffset(0)] public DropEvent Drop;
}
