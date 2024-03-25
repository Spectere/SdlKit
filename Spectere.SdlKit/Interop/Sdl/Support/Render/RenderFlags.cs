namespace Spectere.SdlKit.Interop.Sdl.Support.Render; 

/// <summary>
/// Flags used when creating a rendering context.
/// </summary>
[Flags]
internal enum RendererFlags : uint {
    /// <summary>The renderer is a software fallback.</summary>
    Software = 0x00000001,

    /// <summary>The renderer uses hardware acceleration.</summary>
    Accelerated = 0x00000002,

    /// <summary>Present is synchronized with the refresh rate.</summary>
    PresentVsync = 0x00000004,

    /// <summary>The renderer supports rendering to a texture.</summary>
    TargetTexture = 0x00000008
}
