using SdlHints = Spectere.SdlKit.Interop.Sdl.Support.Hints;
using Spectere.SdlKit.Interop.Sdl;

namespace Spectere.SdlKit;

/// <summary>
/// Common functions for setting SDL hints.
/// </summary>
internal static class SdlHintHelper {
    /// <summary>
    /// Sets the texture filtering hint. This must be used before a new SDL texture is created.
    /// </summary>
    /// <param name="textureFiltering">The <see cref="TextureFiltering"/> mode that should be set.</param>
    internal static void SetTextureFilteringMode(TextureFiltering textureFiltering) {
        _ = Hints.SetHint(
            SdlHints.RenderScaleQuality.Name,
            textureFiltering == TextureFiltering.Nearest
                ? SdlHints.RenderScaleQuality.Nearest
                : SdlHints.RenderScaleQuality.Linear
        );
    }
}
