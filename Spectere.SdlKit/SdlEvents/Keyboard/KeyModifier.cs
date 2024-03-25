namespace Spectere.SdlKit.SdlEvents.Keyboard; 

/// <summary>
/// Contains a list of keyboard modifiers used by SDL2.
/// </summary>
[Flags]
public enum KeyModifier : ushort {
    /// <summary>No keyboard modifiers.</summary>
    None = 0x0000,

    /// <summary>Left shift.</summary>
    LShift = 0x0001,

    /// <summary>Right shift.</summary>
    RShift = 0x0002,

    /// <summary>Left control.</summary>
    LCtrl = 0x0040,

    /// <summary>Right control.</summary>
    RCtrl = 0x0080,

    /// <summary>Left alt.</summary>
    LAlt = 0x0100,

    /// <summary>Right alt.</summary>
    RAlt = 0x0200,

    /// <summary>Left GUI key.</summary>
    LGui = 0x0400,

    /// <summary>Right GUI key.</summary>
    RGui = 0x0800,

    /// <summary>Num lock is on.</summary>
    Num = 0x1000,

    /// <summary>Caps lock is on.</summary>
    Caps = 0x2000,
    
    /// <summary>Unknown.</summary>
    Mode = 0x4000,

    /// <summary>Reserved.</summary>
    Reserved = 0x8000,

    /// <summary>Either control key.</summary>
    Ctrl = LCtrl | RCtrl,

    /// <summary>Either shift key.</summary>
    Shift = LShift | RShift,

    /// <summary>Either alt key.</summary>
    Alt = LAlt | RAlt,

    /// <summary>Either GUI key.</summary>
    Gui = LGui | RGui
}
