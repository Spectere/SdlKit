namespace Spectere.SdlKit.Interop.Sdl.Support.Pixels; 

/// <summary>
/// Indicates the layout of a packed pixel.
/// </summary>
internal enum PackedComponentLayout {
    /// <summary>Format is not packed.</summary>
    LayoutNone,
    
    /// <summary>8-bit 3/3/2 packed format.</summary>
    Layout332,
    
    /// <summary>16-bit 4/4/4/4 packed format.</summary>
    Layout4444,
    
    /// <summary>16-bit 1/5/5/5 packed format.</summary>
    Layout1555,
    
    /// <summary>16-bit 5/5/5/1 packed format.</summary>
    Layout5551,
    
    /// <summary>16-bit 5/6/5 packed format.</summary>
    Layout565,
    
    /// <summary>32-bit 8/8/8/8 packed format.</summary>
    Layout8888,
    
    /// <summary>32-bit 2/10/10/10 packed format.</summary>
    Layout2101010,
    
    /// <summary>32-bit 10/10/10/2 packed format.</summary>
    Layout1010102
}
