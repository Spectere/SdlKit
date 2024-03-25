namespace Spectere.SdlKit;

/// <summary>
/// The texture filtering mode that should be used when displaying an <see cref="IRenderable"/>.
/// </summary>
public enum TextureFiltering {
    /// <summary>
    /// Nearest-neighbor sampling. This will cause scaled textures to generally appear pixelated. This is the
    /// default setting.
    /// </summary>
    Nearest,
    
    /// <summary>
    /// Linear filtering. This will result in a smoother, but potentially blurrier, appearance.
    /// </summary>
    Linear
}
