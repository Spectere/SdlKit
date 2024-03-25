namespace Spectere.SdlKit.Interop.Sdl.Support.Hints;

/// <summary>
/// Represents a variable controlling the scaling quality.
/// </summary>
internal static class RenderScaleQuality {
    /// <summary>
    /// The name of this hint.
    /// </summary>
    internal static string Name => "SDL_RENDER_SCALE_QUALITY";

    /// <summary>
    /// Nearest pixel sampling. This is the default setting for this hint.
    /// </summary>
    internal static string Nearest => "0";

    /// <summary>
    /// Linear filtering (supported by OpenGL and Direct3D).
    /// </summary>
    internal static string Linear => "1";

    /// <summary>
    /// Anisotropic filtering (supported by Direct3D).
    /// </summary>
    internal static string Best => "2";
}
