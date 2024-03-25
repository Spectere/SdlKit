namespace Spectere.SdlKit.Interop.Sdl.Support.Render; 

/// <summary>
/// The access pattern allowed for a texture.
/// </summary>
internal enum TextureAccess {
    /// <summary>Changes rarely, not lockable.</summary>
    Static,

    /// <summary>Changes frequently, lockable.</summary>
    Streaming,

    /// <summary>Texture can be used as a render target.</summary>
    Target
}
