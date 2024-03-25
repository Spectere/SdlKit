namespace Spectere.SdlKit.Interop.Sdl.Support.Pixels; 

/// <summary>
/// Indicates the way pixels are packed.
/// </summary>
internal enum PixelTypes {
    /// <summary>Unknown pixel type.</summary>
    Unknown,
    
    /// <summary>Indexed 1-bit format.</summary>
    Index1,
    
    /// <summary>Indexed 4-bit format.</summary>
    Index4,
    
    /// <summary>Indexed 8-bit format.</summary>
    Index8,
    
    /// <summary>Packed 8-bit format.</summary>
    Packed8,
    
    /// <summary>Packed 16-bit format.</summary>
    Packed16,
    
    /// <summary>Packed 32-bit format.</summary>
    Packed32,
    
    /// <summary>8-bit unsigned integer array format.</summary>
    ArrayU8,
    
    /// <summary>16-bit unsigned integer array format.</summary>
    ArrayU16,
    
    /// <summary>32-bit unsigned integer array format.</summary>
    ArrayU32,
    
    /// <summary>16-bit floating point array format.</summary>
    ArrayF16,
    
    /// <summary>32-bit floating point array format.</summary>
    ArrayF32
}
