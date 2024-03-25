using System.Diagnostics.CodeAnalysis;

namespace Spectere.SdlKit.SdlEvents.Keyboard; 

/// <summary>
/// Represents the state of a given key in a keyboard-related event.
/// </summary>
[SuppressMessage("ReSharper", "UnassignedField.Global")]
public struct Keysym {
    // Since this is used for interop, suppress the "never assigned to" warning.
    #pragma warning disable 0649

    /// <summary>
    /// SDL physical key code.
    /// </summary>
    public Scancode Scancode;
            
    /// <summary>
    /// SDL virtual key code.
    /// </summary>
    public Keycode Sym;

    /// <summary>
    /// Current key modifiers.
    /// </summary>
    public KeyModifier Mod;
    
    public uint Unused;

    #pragma warning restore 0649
}
