using Spectere.SdlKit.Renderables;

namespace Spectere.SdlKit;

/// <summary>
/// The access pattern requested by a particular image.
/// </summary>
public enum ImageType {
    /// <summary>
    /// This should be used for <see cref="Image"/> objects whose pixel data rarely changes, such as static background
    /// images and sprite sheets. This should be used most of the time.
    /// </summary>
    Static,
    
    /// <summary>
    /// This should be used for <see cref="Image"/> objects whose pixel data updates frequently. This is appropriate
    /// if the game or application handles low-level pixel drawing by itself, such as in the case of software 3D
    /// renderers, paint programs, or video players.
    /// </summary>
    Streaming
}
