namespace Spectere.SdlKit.Interop.SdlImage.Support.Image;

/// <summary>
/// Initialization flags.
/// </summary>
[Flags]
public enum InitFlags {
    /// <summary>
    /// No support specified. Passing this to <see cref="Image.Init"/> will return all of the currently initialized
    /// flags.
    /// </summary>
    None = 0x00,
    
    /// <summary>
    /// JPEG support.
    /// </summary>
    InitJpg = 0x01,
    
    /// <summary>
    /// PNG support.
    /// </summary>
    InitPng = 0x02,
    
    /// <summary>
    /// TIFF support.
    /// </summary>
    InitTif = 0x04,
    
    /// <summary>
    /// WebP support.
    /// </summary>
    InitWebP = 0x08,
    
    /// <summary>
    /// JPEG XL support.
    /// </summary>
    InitJxl = 0x10,
    
    /// <summary>
    /// AVIF support.
    /// </summary>
    InitAvif = 0x20
}
