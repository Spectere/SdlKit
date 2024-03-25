namespace Spectere.SdlKit.SdlEvents;

/// <summary>
/// Indicates the type of event.
/// </summary>
public enum SdlEventType : uint {
    /// <summary>Unused.</summary>
    FirstEvent = 0,

    
    /*
     * Application Events
     */
    /// <summary>User-requested quit.</summary>
    Quit = 0x0100,

    /// <summary>The application is being terminated by the OS. Called on iOS in applicationWillTerminate(); called on Android in onDestroy().</summary>
    AppTerminating,

    /// <summary>The application is low on memory, free memory if possible. Called on iOS in applicationDidReceiveMemoryWarning(); called on Android in onLowMemory().</summary>
    LowMemory,

    /// <summary>The application is about to enter the background. Called on iOS in applicationWillResignActive(); called on Android in onPause().</summary>
    WillEnterBackground,

    /// <summary>The application did enter the background and may not get CPU for some time. Called on iOS in applicationDidEnterBackground(); called on Android in onPause().</summary>
    DidEnterBackground,

    /// <summary>The application is about to enter the foreground. Called on iOS in applicationWillEnterForeground(); called on Android in onResume().</summary>
    WillEnterForeground,

    /// <summary>The application is now interactive. Called on iOS in applicationDidBecomeActive(); called on Android in onResume().</summary>
    DidEnterForeground,
    
    /// <summary>The user's locale preferences have changed.</summary>
    LocaleChanged,

    
    /*
     * Window events.
     */
    /// <summary>Window state change.</summary>
    WindowEvent = 0x0200,

    /// <summary>System-specific event.</summary>
    SysWmEvent,

    
    /*
     * Keyboard events.
     */
    /// <summary>Key pressed.</summary>
    KeyDown = 0x0300,

    /// <summary>Key released.</summary>
    KeyUp,

    /// <summary>Keyboard text editing (composition).</summary>
    TextEditing,

    /// <summary>Keyboard text input.</summary>
    TextInput,

    /// <summary>Keymap changed due to a system event such as an input language or keyboard layout change.</summary>
    KeymapChanged,
    
    /// <summary>Extended keyboard text editing (composition).</summary>
    TextEditingExt,
    
    
    /*
     * Mouse events.
     */
    /// <summary>Mouse moved.</summary>
    MouseMotion = 0x0400,

    /// <summary>Mouse button pressed.</summary>
    MouseButtonDown,

    /// <summary>Mouse button released.</summary>
    MouseButtonUp,

    /// <summary>Mouse wheel motion.</summary>
    MouseWheel,

    
    /*
     * Joystick events.
     */
    /// <summary>Joystick axis motion.</summary>
    JoyAxisMotion = 0x0600,

    /// <summary>Joystick trackball motion.</summary>
    JoyBallMotion,

    /// <summary>Joystick hat position change.</summary>
    JoyHatMotion,

    /// <summary>Joystick button pressed.</summary>
    JoyButtonDown,

    /// <summary>Joystick button released.</summary>
    JoyButtonUp,

    /// <summary>A new joystick has been inserted into the system.</summary>
    JoyDeviceAdded,

    /// <summary>An opened joystick has been removed.</summary>
    JoyDeviceRemoved,
    
    /// <summary>Joystick battery level change.</summary>
    JoyBatteryUpdated,

    
    /*
     * Game controller events.
     */
    /// <summary>Game controller axis motion.</summary>
    GameControllerAxisMotion = 0x0650,

    /// <summary>Game controller button pressed.</summary>
    GameControllerButtonDown,

    /// <summary>Game controller button released.</summary>
    GameControllerButtonUp,

    /// <summary>A new game controller has been inserted into the system.</summary>
    GameControllerDeviceAdded,

    /// <summary>An opened game controller has been removed.</summary>
    GameControllerDeviceRemoved,

    /// <summary>The controller mapping was updated.</summary>
    GameControllerDeviceRemapped,
    
    /// <summary>Game controller touchpad was touched.</summary>
    GameControllerTouchpadDown,
    
    /// <summary>Game controller touchpad finger was moved.</summary>
    GameControllerTouchpadMotion,
    
    /// <summary>Game controller touchpad finger was lifted.</summary>
    GameControllerTouchpadUp,
    
    /// <summary>Game controller sensor was updated.</summary>
    GameControllerSensorUpdate,

    
    /*
     * Touch events.
     */
    /// <summary>The display has been touched..</summary>
    FingerDown = 0x0700,

    /// <summary>The display has been released.</summary>
    FingerUp,

    /// <summary>Finger motion was detected by the display.</summary>
    FingerMotion,

    
    /*
     * Gesture events.
     */
    /// <summary>A gesture has been performed.</summary>
    DollarGesture = 0x0800,

    /// <summary>A gesture has been recorded.</summary>
    DollarRecord,

    /// <summary>A multitouch gesture has been performed.</summary>
    MultiGesture,

    
    /*
     * Clipboard events.
     */
    /// <summary>The clipboard changed.</summary>
    ClipboardUpdate = 0x0900,

    
    /*
     * Drag and drop events.
     */
    /// <summary>The system requests a file open.</summary>
    DropFile = 0x1000,

    /// <summary>text/plain drag-and-drop event.</summary>
    DropText,

    /// <summary>A new set of drops is beginning (NULL filename).</summary>
    DropBegin,

    /// <summary>Current set of drops is now complete (NULL Filename).</summary>
    DropComplete,

    
    /*
     * Audio hot-plug events.
     */
    /// <summary>A new audio device is available.</summary>
    AudioDeviceAdded = 0x1100,

    /// <summary>An audio device has been removed.</summary>
    AudioDeviceRemoved,
    
    
    /*
     * Sensor events.
     */
    /// <summary>A sensor was updated.</summary>
    SensorUpdate = 0x1200,

    
    /*
     * Render events.
     */
    /// <summary>The render targets have been reset and their contents need to be updated.</summary>
    RenderTargetsReset = 0x2000,

    /// <summary>The device has been reset and all textures need to be recreated.</summary>
    RenderDeviceReset,
    
    
    /*
     * Internal events.
     */
    /// <summary>Signals the end of an event poll cycle.</summary>
    PollSentinel = 0x7F00,

    
    /*
     * Other events.
     */
    /// <summary>User events.</summary>
    UserEvent = 0x8000,

    /// <summary>Unused.</summary>
    LastEvent = 0xFFFF
}
