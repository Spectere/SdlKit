namespace Spectere.SdlKit;

/// <summary>
/// Describes the type of gamepad that's plugged into the system.
/// </summary>
public enum GamepadType {
    /// <summary>
    /// The controller type is unknown.
    /// </summary>
    Unknown,
    
    /// <summary>
    /// Microsoft Xbox 360 controller.
    /// </summary>
    Xbox360,
    
    /// <summary>
    /// Microsoft Xbox One or Series controller.
    /// </summary>
    XboxOne,
    
    /// <summary>
    /// Sony PlayStation 3 controller.
    /// </summary>
    PlayStation3,
    
    /// <summary>
    /// Sony PlayStation 4 controller.
    /// </summary>
    PlayStation4,
    
    /// <summary>
    /// Nintendo Switch Pro controller.
    /// </summary>
    NintendoSwitchPro,
    
    /// <summary>
    /// ???
    /// </summary>
    Virtual,
    
    /// <summary>
    /// Sony PlayStation 5 controller.
    /// </summary>
    PlayStation5,
    
    /// <summary>
    /// Amazon Luna controller.
    /// </summary>
    AmazonLuna,
    
    /// <summary>
    /// Google Stadia controller.
    /// </summary>
    GoogleStadia,
    
    /// <summary>
    /// NVIDIA Shield controller.
    /// </summary>
    NvidiaShield,
    
    /// <summary>
    /// Nintendo Switch left Joy-Con controller.
    /// </summary>
    NintendoSwitchJoyConLeft,
    
    /// <summary>
    /// Nintendo Switch right Joy-Con controller.
    /// </summary>
    NintendoSwitchJoyConRight,
    
    /// <summary>
    /// Nintendo Switch Joy-Con controllers.
    /// </summary>
    NintendoSwitchJoyConPair
}
