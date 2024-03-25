using Spectere.SdlKit.EventArgs.Gamepad;
using Spectere.SdlKit.EventArgs.Keyboard;
using Spectere.SdlKit.EventArgs.Mouse;
using Spectere.SdlKit.EventArgs.Window;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.SdlEvents;
using Spectere.SdlKit.SdlEvents.Window;

namespace Spectere.SdlKit;

public partial class Window {
    #region Gamepad Events

    /// <summary>
    /// Raised when an analog axis is updated or a d-pad input is performed on a gamepad.
    /// </summary>
    public event EventHandler<GamepadAxisEventArgs>? OnGamepadAxisMotion;

    /// <summary>
    /// Raised when a button is pressed on a gamepad.
    /// </summary>
    public event EventHandler<GamepadButtonEventArgs>? OnGamepadButtonDown;
    
    /// <summary>
    /// Raised when a button is released on a gamepad.
    /// </summary>
    public event EventHandler<GamepadButtonEventArgs>? OnGamepadButtonUp;

    /// <summary>
    /// Raised when a gamepad is plugged into the system.
    /// </summary>
    public event EventHandler<GamepadDeviceEventArgs>? OnGamepadDeviceAdded;
    
    /// <summary>
    /// Raised when a gamepad is remapped.
    /// </summary>
    public event EventHandler<GamepadDeviceEventArgs>? OnGamepadDeviceRemapped;

    /// <summary>
    /// Raised when a gamepad is unplugged from the system.
    /// </summary>
    public event EventHandler<GamepadDeviceEventArgs>? OnGamepadDeviceRemoved;

    /// <summary>
    /// Raised when a gamepad's sensor (such as an accelerometer or gyroscope) is updated.
    /// </summary>
    public event EventHandler<GamepadSensorUpdateEventArgs>? OnGamepadSensorUpdate; 

    /// <summary>
    /// Raised when the user places a finger on a gamepad's touchpad.
    /// </summary>
    public event EventHandler<GamepadTouchpadEventArgs>? OnGamepadTouchpadDown;
    
    /// <summary>
    /// Raised when the user moves their finger along the surface of a gamepad's touchpad.
    /// </summary>
    public event EventHandler<GamepadTouchpadEventArgs>? OnGamepadTouchpadMotion;

    /// <summary>
    /// Raised when the user released their finger from a gamepad's touchpad.
    /// </summary>
    public event EventHandler<GamepadTouchpadEventArgs>? OnGamepadTouchpadUp;
    
    #endregion
    
    #region Keyboard Events

    /// <summary>
    /// Raised when the user presses a key on the keyboard.
    /// </summary>
    public event EventHandler<KeyEventArgs>? OnKeyDown;

    /// <summary>
    /// Raised when the user releases a key on the keyboard.
    /// </summary>
    public event EventHandler<KeyEventArgs>? OnKeyUp;
    
    #endregion
    
    #region Mouse Events

    /// <summary>
    /// Raised when the user presses a mouse button.
    /// </summary>
    public event EventHandler<MouseButtonEventArgs>? OnMouseButtonDown;
    
    /// <summary>
    /// Raised when a user releases a mouse button.
    /// </summary>
    public event EventHandler<MouseButtonEventArgs>? OnMouseButtonUp;

    /// <summary>
    /// Raised when the mouse pointer moves within the window.
    /// </summary>
    public event EventHandler<MouseMotionEventArgs>? OnMouseMotion;

    /// <summary>
    /// Raised when the user moves the scroll wheel on the mouse.
    /// </summary>
    public event EventHandler<MouseWheelEventArgs>? OnMouseWheelMoved;
    
    #endregion
    
    #region Window Events
    
    /// <summary>
    /// Raised when the window manager requests the window to closed.
    /// </summary>
    public event EventHandler<WindowCloseEventArgs>? OnWindowClose;

    /// <summary>
    /// Raised when the window is hidden.
    /// </summary>
    public event EventHandler<WindowHiddenEventArgs>? OnWindowHidden;
    
    /// <summary>
    /// Raised when the window gains keyboard focus.
    /// </summary>
    public event EventHandler<WindowKeyboardFocusGainedEventArgs>? OnWindowKeyboardFocusGained;
    
    /// <summary>
    /// Raised when the window loses keyboard focus.
    /// </summary>
    public event EventHandler<WindowKeyboardFocusLostEventArgs>? OnWindowKeyboardFocusLost;
    
    /// <summary>
    /// Raised when the window is maximized.
    /// </summary>
    public event EventHandler<WindowMaximizedEventArgs>? OnWindowMaximized;
    
    /// <summary>
    /// Raised when the window is minimized.
    /// </summary>
    public event EventHandler<WindowMinimizedEventArgs>? OnWindowMinimized;
    
    /// <summary>
    /// Raised when the window is moved.
    /// </summary>
    public event EventHandler<WindowMovedEventArgs>? OnWindowMoved;
    
    /// <summary>
    /// Raised when the window gains mouse focus.
    /// </summary>
    public event EventHandler<WindowMouseFocusGainedEventArgs>? OnWindowMouseFocusGained;
    
    /// <summary>
    /// Raised when the window loses mouse focus.
    /// </summary>
    public event EventHandler<WindowMouseFocusLostEventArgs>? OnWindowMouseFocusLost;
    
    /// <summary>
    /// Raised when the window is resized by the user.
    /// </summary>
    public event EventHandler<WindowResizedEventArgs>? OnWindowResized;
    
    /// <summary>
    /// Raised when the window is restored to its normal size and position.
    /// </summary>
    public event EventHandler<WindowRestoredEventArgs>? OnWindowRestored;

    /// <summary>
    /// Raised when the window is shown.
    /// </summary>
    public event EventHandler<WindowShownEventArgs>? OnWindowShown;
    
    /// <summary>
    /// Raised when the window is resized by the user, the system, or via an API call.
    /// </summary>
    public event EventHandler<WindowSizeChangedEventArgs>? OnWindowSizeChanged;
    
    /// <summary>
    /// Dispatches a window event to the appropriate event handler.
    /// </summary>
    /// <param name="ev">The SDL window event to process.</param>
    private void ProcessWindowEvent(WindowEvent ev) {
        switch(ev.EventId) {
            // The window manager requests that the window be closed.
            case WindowEventType.Close:
                OnWindowClose?.Invoke(this, new WindowCloseEventArgs { SdlEvent = ev });
                break;
            
            // Window has gained mouse focus.
            case WindowEventType.Enter:
                OnWindowMouseFocusGained?.Invoke(this, new WindowMouseFocusGainedEventArgs { SdlEvent = ev });
                break;
            
            // Window has gained keyboard focus.
            case WindowEventType.FocusGained:
                OnWindowKeyboardFocusGained?.Invoke(this, new WindowKeyboardFocusGainedEventArgs { SdlEvent = ev });
                break;
            
            // Window has lost keyboard focus.
            case WindowEventType.FocusLost:
                OnWindowKeyboardFocusLost?.Invoke(this, new WindowKeyboardFocusLostEventArgs { SdlEvent = ev });
                break;
            
            // Window has been hidden.
            case WindowEventType.Hidden:
                OnWindowHidden?.Invoke(this, new WindowHiddenEventArgs { SdlEvent = ev });
                break;
            
            // Window has lost mouse focus.
            case WindowEventType.Leave:
                OnWindowMouseFocusLost?.Invoke(this, new WindowMouseFocusLostEventArgs { SdlEvent = ev });
                break;
            
            // Window has been maximized.
            case WindowEventType.Maximized:
                OnWindowMaximized?.Invoke(this, new WindowMaximizedEventArgs { SdlEvent = ev });
                break;
            
            // Window has been minimized.
            case WindowEventType.Minimized:
                OnWindowMinimized?.Invoke(this, new WindowMinimizedEventArgs { SdlEvent = ev });
                break;
            
            // Window has been moved.
            case WindowEventType.Moved:
                OnWindowMoved?.Invoke(this, new WindowMovedEventArgs { SdlEvent = ev });
                break;
            
            // Window has been resized by the user.
            case WindowEventType.Resized:
                OnWindowResized?.Invoke(this, new WindowResizedEventArgs { SdlEvent = ev });
                break;
            
            // Window has been restored to its normal size and position.
            case WindowEventType.Restored:
                OnWindowRestored?.Invoke(this, new WindowRestoredEventArgs { SdlEvent = ev });
                break;
            
            // Window has been shown.
            case WindowEventType.Shown:
                OnWindowShown?.Invoke(this, new WindowShownEventArgs { SdlEvent = ev });
                break;
            
            // Window size has changed by the user, the system, or via API call.
            case WindowEventType.SizeChanged:
                OnWindowSizeChanged?.Invoke(this, new WindowSizeChangedEventArgs { SdlEvent = ev });
                break;
            
            // Unhandled events.
            case WindowEventType.Exposed:
            case WindowEventType.HitTest:
            case WindowEventType.None:
            case WindowEventType.TakeFocus:
            default:
                break;
        }
    }

    #endregion
    
    /// <summary>
    /// Processes all of the queued events that have occurred since this method was last called. This should be called
    /// regularly, as it's required for all events (such as <see cref="OnWindowClose"/>) to be sent to the application.
    /// </summary>
    protected void ProcessEvents() {
        while(Events.PollEvent(out var ev) != 0) {
            switch(ev.Type) {
                //
                // Gamepad
                //
                case SdlEventType.GameControllerAxisMotion:
                    OnGamepadAxisMotion?.Invoke(this, new GamepadAxisEventArgs { SdlEvent = ev.GameControllerAxis });
                    break;
                
                case SdlEventType.GameControllerButtonDown:
                    OnGamepadButtonDown?.Invoke(this, new GamepadButtonEventArgs { SdlEvent = ev.GameControllerButton });
                    break;
                
                case SdlEventType.GameControllerButtonUp:
                    OnGamepadButtonUp?.Invoke(this, new GamepadButtonEventArgs { SdlEvent = ev.GameControllerButton });
                    break;
                    
                case SdlEventType.GameControllerDeviceAdded:
                    OnGamepadDeviceAdded?.Invoke(this, new GamepadDeviceEventArgs { SdlEvent = ev.GameControllerDevice });
                    break;
                
                case SdlEventType.GameControllerDeviceRemapped:
                    OnGamepadDeviceRemapped?.Invoke(this, new GamepadDeviceEventArgs { SdlEvent = ev.GameControllerDevice });
                    break;

                case SdlEventType.GameControllerDeviceRemoved:
                    OnGamepadDeviceRemoved?.Invoke(this, new GamepadDeviceEventArgs { SdlEvent = ev.GameControllerDevice });
                    break;

                case SdlEventType.GameControllerSensorUpdate:
                    OnGamepadSensorUpdate?.Invoke(this, new GamepadSensorUpdateEventArgs { SdlEvent = ev.GameControllerSensor });
                    break;

                case SdlEventType.GameControllerTouchpadDown:
                    OnGamepadTouchpadDown?.Invoke(this, new GamepadTouchpadEventArgs { SdlEvent = ev.GameControllerTouchpad });
                    break;
                
                case SdlEventType.GameControllerTouchpadMotion:
                    OnGamepadTouchpadMotion?.Invoke(this, new GamepadTouchpadEventArgs { SdlEvent = ev.GameControllerTouchpad });
                    break;
                
                case SdlEventType.GameControllerTouchpadUp:
                    OnGamepadTouchpadUp?.Invoke(this, new GamepadTouchpadEventArgs { SdlEvent = ev.GameControllerTouchpad });
                    break;
                
                //
                // Keyboard
                //
                case SdlEventType.KeyDown:
                    OnKeyDown?.Invoke(this, new KeyEventArgs { SdlEvent = ev.Keyboard });
                    break;
                
                case SdlEventType.KeyUp:
                    OnKeyUp?.Invoke(this, new KeyEventArgs { SdlEvent = ev.Keyboard });
                    break;
                
                //
                // Mouse
                //
                case SdlEventType.MouseButtonDown:
                    OnMouseButtonDown?.Invoke(this, new MouseButtonEventArgs { SdlEvent = ev.MouseButton });
                    break;

                case SdlEventType.MouseButtonUp:
                    OnMouseButtonUp?.Invoke(this, new MouseButtonEventArgs { SdlEvent = ev.MouseButton });
                    break;
                
                case SdlEventType.MouseMotion:
                    OnMouseMotion?.Invoke(this, new MouseMotionEventArgs { SdlEvent = ev.MouseMotion });
                    break;
                
                case SdlEventType.MouseWheel:
                    OnMouseWheelMoved?.Invoke(this, new MouseWheelEventArgs { SdlEvent = ev.MouseWheel });
                    break;

                //
                // Window
                //
                case SdlEventType.WindowEvent:
                    ProcessWindowEvent(ev.Window);
                    break;
                
                // The Great Unprocessed(TM)
                case SdlEventType.AppTerminating:
                case SdlEventType.AudioDeviceAdded:
                case SdlEventType.AudioDeviceRemoved:
                case SdlEventType.ClipboardUpdate:
                case SdlEventType.DidEnterBackground:
                case SdlEventType.DidEnterForeground:
                case SdlEventType.DollarGesture:
                case SdlEventType.DollarRecord:
                case SdlEventType.DropBegin:
                case SdlEventType.DropComplete:
                case SdlEventType.DropFile:
                case SdlEventType.DropText:
                case SdlEventType.FingerDown:
                case SdlEventType.FingerUp:
                case SdlEventType.FingerMotion:
                case SdlEventType.FirstEvent:
                case SdlEventType.JoyAxisMotion:
                case SdlEventType.JoyBallMotion:
                case SdlEventType.JoyBatteryUpdated:
                case SdlEventType.JoyButtonDown:
                case SdlEventType.JoyButtonUp:
                case SdlEventType.JoyDeviceAdded:
                case SdlEventType.JoyDeviceRemoved:
                case SdlEventType.JoyHatMotion:
                case SdlEventType.KeymapChanged:
                case SdlEventType.LastEvent:
                case SdlEventType.LocaleChanged:
                case SdlEventType.LowMemory:
                case SdlEventType.MultiGesture:
                case SdlEventType.Quit:
                case SdlEventType.PollSentinel:
                case SdlEventType.RenderDeviceReset:
                case SdlEventType.RenderTargetsReset:
                case SdlEventType.SensorUpdate:
                case SdlEventType.SysWmEvent:
                case SdlEventType.TextEditing:
                case SdlEventType.TextEditingExt:
                case SdlEventType.TextInput:
                case SdlEventType.UserEvent:
                case SdlEventType.WillEnterBackground:
                case SdlEventType.WillEnterForeground:
                default:
                    break;
            }
        }
    }
}
