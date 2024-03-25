namespace Spectere.SdlKit.Interop.Sdl.Support.Keyboard; 

/// <summary>
/// Contains constants used for scancode conversion.
/// </summary>
internal static class KeycodeConstants {
    /// <summary>A mask applied to keycodes that must be accessed via scancode.</summary>
    internal const int ScancodeMask = 1 << 30;
}
