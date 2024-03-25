namespace Spectere.SdlKit.SdlEvents.GameController;

/// <summary>
/// The buttons that are supported by SDL2's game controller subsystem.
/// </summary>
public enum GameControllerButton : sbyte {
    /// <summary>Invalid button.</summary>
    Invalid = -1,

    /// <summary>The controller's A button (Cross on PlayStation controllers).</summary>
    A,

    /// <summary>The controller's B button (Circle on PlayStation controllers).</summary>
    B,

    /// <summary>The controller's X button (Square on PlayStation controllers).</summary>
    X,

    /// <summary>The controller's Y button (Triangle on PlayStation controllers).</summary>
    Y,

    /// <summary>The controller's back button (View on the Xbox One/Series controller, Share on the PS4/PS5 controllers).</summary>
    Back,

    /// <summary>The controller's guide button (usually the Xbox or PlayStation logos).</summary>
    Guide,

    /// <summary>The controller's start button (Menu on the Xbox One/Series controller, Options on the PS4/PS5 controllers).</summary>
    Start,

    /// <summary>The left stick button, also known as L3.</summary>
    LeftStick,

    /// <summary>The right stick button, also known as R3.</summary>
    RightStick,

    /// <summary>The left shoulder button, also known as LB.</summary>
    LeftShoulder,

    /// <summary>The right shoulder button, also known as RB.</summary>
    RightShoulder,

    /// <summary>Up on the directional pad.</summary>
    Up,

    /// <summary>Down on the direction pad.</summary>
    Down,

    /// <summary>Left on the directional pad.</summary>
    Left,

    /// <summary>Right on the directional pad.</summary>
    Right,
    
    /// <summary>The controller's share button (only applicable to the the Xbox Series controller).</summary>
    Share
}
