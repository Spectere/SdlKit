namespace Spectere.SdlKit.Interop.Sdl.Support.Init; 

/// <summary>
/// Represents an SDL2 subsystem.
/// </summary>
[Flags]
internal enum SubsystemFlags : uint {
    /// <summary>No subsystem.</summary>
    None = 0x00000000,

    /// <summary>Timer subsystem.</summary>
    Timer = 0x00000001,

    /// <summary>Audio subsystem.</summary>
    Audio = 0x00000010,

    /// <summary>Video subsystem; automatically initializes the events subsystem.</summary>
    Video = 0x00000020,

    /// <summary>Joystick subsystem; automatically initializes the events subsystem.</summary>
    Joystick = 0x00000200,

    /// <summary>Haptic (force feedback) subsystem.</summary>
    Haptic = 0x00001000,

    /// <summary>Controller subsystem; automatically initializes the joystick subsystem.</summary>
    GameController = 0x00002000,

    /// <summary>Events subsystem.</summary>
    Events = 0x00004000,
    
    /// <summary>Sensor subsystem.</summary>
    Sensor = 0x00008000,

    /// <summary>Compatibility; this flag is ignored.</summary>
    NoParachute = 0x00100000,

    /// <summary>All of the available subsystems.</summary>
    Everything = Timer | Audio | Video | Joystick | Haptic | GameController | Events | NoParachute
}
