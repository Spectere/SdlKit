namespace Spectere.SdlKit.Interop.Sdl.Support.Render; 

/// <summary>
/// The scaling mode for a texture.
/// </summary>
internal enum ScaleMode {
    /// <summary>Nearest-neighbor sampling (no filtering).</summary>
    Nearest,
    
    /// <summary>Linear filtering.</summary>
    Linear,
    
    /// <summary>Anisotropic filtering.</summary>
    Best
}
