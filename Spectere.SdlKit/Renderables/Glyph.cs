namespace Spectere.SdlKit.Renderables;

/// <summary>
/// Represents a single glyph in an <see cref="TextConsole"/>.
/// </summary>
public struct Glyph {
    /// <summary>
    /// The foreground color of this <see cref="Glyph"/>.
    /// </summary>
    public SdlColor ForegroundColor;

    /// <summary>
    /// The background color of this <see cref="Glyph"/>.
    /// </summary>
    public SdlColor BackgroundColor;
    
    /// <summary>
    /// The character index of this glyph.
    /// </summary>
    public int GlyphIndex;
}
