namespace Spectere.SdlKit;

/// <summary>
/// Stores information about a loaded texture.
/// </summary>
/// <param name="TextureWidth">The width of the texture, in pixels.</param>
/// <param name="TextureHeight">The height of the texture, in pixels.</param>
internal record TextureInformation(
    int TextureWidth,
    int TextureHeight
);
