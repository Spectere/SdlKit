using System.Diagnostics.CodeAnalysis;

namespace Spectere.SdlKit.Interop.Sdl.Support.Pixels; 

/// <summary>
/// The order of the components in a pixel.
/// </summary>
[SuppressMessage("Design", "CA1069:Enums values should not be duplicated")]
internal enum ComponentOrder {
    None = 0x00,

    
    /*
     * Bitmap pixel order.
     */
    /// <summary>Reverse bitmap pixel order.</summary>
    Bitmap4321 = 0x01,
    
    /// <summary>Sequential bitmap pixel order.</summary>
    Bitmap1234 = 0x02,
    
    
    /*
     * Packed component order.
     */
    /// <summary>Packed XRGB format.</summary>
    PackedXrgb = 0x01,
    
    /// <summary>Packed RGBX format.</summary>
    PackedRgbx = 0x02,
    
    /// <summary>Packed ARGB format.</summary>
    PackedArgb = 0x03,
    
    /// <summary>Packed RGBA format.</summary>
    PackedRgba = 0x04,
    
    /// <summary>Packed XBGR format.</summary>
    PackedXbgr = 0x05,
    
    /// <summary>Packed BGRX format.</summary>
    PackedBgrx = 0x06,
    
    /// <summary>Packed ABGR format.</summary>
    PackedAbgr = 0x07,
    
    /// <summary>Packed BGRA format.</summary>
    PackedBgra = 0x08,

    
    /*
     * Array component order.
     */
    /// <summary>RGB array format.</summary>
    ArrayRgb = 0x01,
    
    /// <summary>RGBA array format.</summary>
    ArrayRgba = 0x02,
    
    /// <summary>ARGB array format.</summary>
    ArrayArgb = 0x03,
    
    /// <summary>BGR array format.</summary>
    ArrayBgr = 0x04,
    
    /// <summary>BGRA array format.</summary>
    ArrayBgra = 0x05,
    
    /// <summary>ABGR array format.</summary>
    ArrayAbgr = 0x06
}
