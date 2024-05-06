using SdlImage = Spectere.SdlKit.Interop.SdlImage.Image;
using Spectere.SdlKit.Exceptions;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.Render;
using System.Runtime.CompilerServices;

namespace Spectere.SdlKit.Renderables;

/// <summary>
/// Defines an SDL text console. This cannot be drawn to directly and is instead made up of a set of characters from
/// a given character set.
/// </summary>
public partial class TextConsole : Renderable {
    /// <summary>
    /// The console buffer.
    /// </summary>
    private Glyph[] _consoleBuffer = [];
    
    /// <summary>
    /// The current position of the cursor within the console buffer.
    /// </summary>
    private int _cursorPosition;

    /// <summary>
    /// The number of glyphs present in the loaded font.
    /// </summary>
    private int _fontGlyphCount;
    
    /// <summary>
    /// The number of glyphs per row of the font texture.
    /// </summary>
    private int _fontGlyphsPerRow;

    /// <summary>
    /// The height of each glyph in the loaded font.
    /// </summary>
    private int _fontGlyphHeight;

    /// <summary>
    /// The width of each glyph in the loaded font.
    /// </summary>
    private int _fontGlyphWidth;
    
    /// <summary>
    /// An SDL texture that contains the font glyphs.
    /// </summary>
    private SdlTexture _fontTexture;
    
    /// <summary>
    /// The total number of glyphs in the console text area.
    /// </summary>
    private int _consoleTotalGlyphs;

    /// <summary>
    /// The total height of each glyph, including padding.
    /// </summary>
    private int _glyphTotalHeight;

    /// <summary>
    /// The total width of each glyph, including padding.
    /// </summary>
    private int _glyphTotalWidth;

    /// <summary>
    /// The total height of the text area, in pixels.
    /// </summary>
    private int _textAreaHeight;

    /// <summary>
    /// The total width of the text area, in pixels.
    /// </summary>
    private int _textAreaWidth;
    
    /// <summary>
    /// If this is set to <c>true</c> the next <see cref="Update"/> call will redraw the console's padding area.
    /// </summary>
    private bool _updatePadding;

    /// <summary>
    /// If this is set to <c>true</c>, the next <see cref="Update"/> call will redraw the entire text area.
    /// </summary>
    private bool _updateTextArea;

    /// <summary>
    /// Gets or sets the auto-scroll value for this console. If this is set to <c>true</c>, the console will
    /// automatically scroll up when the screen is full. This is useful for debug consoles or terminal simulations.
    /// If this is set to <c>false</c> the cursor will wrap around when it reaches the end of the buffer. This mode is
    /// useful for text-based games. This property defaults to <c>true</c>.
    /// </summary>
    public bool AutoScroll { get; set; } = true;

    /// <summary>
    /// Gets the height of the console, in number of characters. This value is automatically calculated based on the
    /// size of the console (in pixels) and the console and glyph padding.
    /// </summary>
    public int ConsoleHeight { get; private set; }
    
    /// <summary>
    /// Gets the width of the console, in number of characters. This value is automatically calculated based on the
    /// size of the console (in pixels) and the console and glyph padding.
    /// </summary>
    public int ConsoleWidth { get; private set; }
    
    /// <summary>
    /// Gets or sets the padding around the main console. It is highly recommended that you <see cref="Clear"/> the
    /// console before changing this value, as failing to do so can cause existing text to display incorrectly.
    /// </summary>
    public Padding ConsolePadding {
        get => _consolePadding;
        set {
            // Only update if we need to.
            if(_consolePadding == value) {
                return;
            }

            _consolePadding = value;
            RecalculateSizesAndBounds();
        }
    }
    private Padding _consolePadding;

    /// <summary>
    /// The default glyph for this <see cref="TextConsole"/>. All glyphs in this console will be replaced with this
    /// whenever the screen is cleared. If <see cref="PaddingColor"/> is <c>null</c>, the background color for this
    /// glyph will be used as the padding color.
    /// </summary>
    public Glyph DefaultGlyph {
        get => _defaultGlyph;
        set {
            // If the padding color is not specified and the background color changes, redraw the padding area.
            if(PaddingColor is null && _defaultGlyph.BackgroundColor != value.BackgroundColor) {
                _updatePadding = true;
            }

            _defaultGlyph = value;
        }
    }
    private Glyph _defaultGlyph = new() {
        BackgroundColor = new SdlColor(0, 0, 0),
        ForegroundColor = new SdlColor(192, 192, 192),
        GlyphIndex = ' '
    };

    /// <summary>
    /// Gets or sets the padding around glyphs (individual characters). It is highly recommended that you
    /// <see cref="Clear"/> the console before changing this value, as failing to do so can cause existing text to
    /// display incorrectly.
    /// </summary>
    public Padding GlyphPadding {
        get => _glyphPadding;
        set {
            // Only update if we need to.
            if(_glyphPadding == value) {
                return;
            }

            _glyphPadding = value;
            RecalculateSizesAndBounds();
        }
    }
    private Padding _glyphPadding;

    /// <summary>
    /// Gets or sets the color of the padding region for this <see cref="TextConsole"/>. Note that this will not
    /// impact the padding between glyphs.
    /// </summary>
    public SdlColor? PaddingColor {
        get => _paddingColor;
        set {
            if(_paddingColor != value) {
                _updatePadding = true;
            }

            _paddingColor = value;
        }
    }
    private SdlColor? _paddingColor;
    
    /// <summary>
    /// Creates a new <see cref="TextConsole"/>.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> that should be used to create the backing texture.</param>
    /// <param name="width">The width of the <see cref="TextConsole"/>, in pixels.</param>
    /// <param name="height">The height of the <see cref="TextConsole"/>, in pixels.</param>
    /// <param name="fontFilename">The name of the font that should be initially used by this console.</param>
    /// <param name="glyphWidth">The width of each glyph in the font file, in pixels.</param>
    /// <param name="glyphHeight">The height of each glyph in the font file, in pixels.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="TextConsole"/> should use.</param>
    /// <exception cref="SdlTextureInitializationException">Thrown when SDL is unable to create a texture.</exception>
    internal TextConsole(SdlRenderer renderer, int width, int height, string fontFilename, int glyphWidth, int glyphHeight, TextureFiltering textureFiltering = TextureFiltering.Nearest)
        : base(renderer, TextureAccess.Target, width, height, textureFiltering) {
        LoadFont(fontFilename, glyphWidth, glyphHeight);
        Clear();
    }
    
    
    /// <summary>
    /// Creates a new <see cref="TextConsole"/>.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> whose renderer should be used to create the backing texture.</param>
    /// <param name="width">The width of the <see cref="TextConsole"/>, in pixels.</param>
    /// <param name="height">The height of the <see cref="TextConsole"/>, in pixels.</param>
    /// <param name="fontFilename">The name of the font that should be initially used by this console.</param>
    /// <param name="glyphWidth">The width of each glyph in the font file, in pixels.</param>
    /// <param name="glyphHeight">The height of each glyph in the font file, in pixels.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="TextConsole"/> should use.</param>
    /// <exception cref="SdlTextureInitializationException">Thrown when SDL is unable to create a texture.</exception>
    public TextConsole(Window window, int width, int height, string fontFilename, int glyphWidth, int glyphHeight, TextureFiltering textureFiltering = TextureFiltering.Nearest)
        : this(window.SdlRenderer, width, height, fontFilename, glyphWidth, glyphHeight, textureFiltering) { }

    /// <summary>
    /// This will automatically set the padding around the console so that the text is centered. By default, the text
    /// area will be positioned in the upper-left corner of the <see cref="Renderable"/>.
    /// </summary>
    public void CenterTextArea() {
        var leftPadding = (Width - _textAreaWidth) / 2;
        var topPadding = (Height - _textAreaHeight) / 2;

        ConsolePadding = new Padding(leftPadding, 0, topPadding, 0);
    }
    
    /// <summary>
    /// Clears all glyphs from the console, replacing them with <see cref="DefaultGlyph"/>. This also resets the cursor
    /// position to (0, 0).
    /// </summary>
    public void Clear() {
        Array.Fill(_consoleBuffer, DefaultGlyph);
        _updateTextArea = true;
        _cursorPosition = 0;
    }

    /// <inheritdoc/>
    public override void Dispose() {
        base.Dispose();

        if(!_fontTexture.IsNull) {
            Render.DestroyTexture(_fontTexture);
        }
    }

    /// <summary>
    /// Gets the current position of the cursor.
    /// </summary>
    /// <returns>A tuple containing the cursor's X and Y positions, in characters.</returns>
    public (int x, int y) GetCursorPosition() => (_cursorPosition % ConsoleWidth, _cursorPosition / ConsoleWidth);

    /// <summary>
    /// Loads a new font, replacing the existing one associated with this <see cref="TextConsole"/>. It is highly
    /// recommended that you <see cref="Clear"/> the console before changing this value, as failing to do so can cause
    /// existing text to display incorrectly.
    /// </summary>
    /// <param name="fontFilename">The name of the font that should be initially used by this console.</param>
    /// <param name="glyphWidth">The width of each glyph in the font file, in pixels.</param>
    /// <param name="glyphHeight">The height of each glyph in the font file, in pixels.</param>
    /// <exception cref="FileNotFoundException">Thrown when the file passed in via <paramref name="fontFilename"/> does
    /// not exist.</exception>
    public void LoadFont(string fontFilename, int glyphWidth, int glyphHeight) {
        if(!File.Exists(fontFilename)) {
            throw new FileNotFoundException($"Font could not be found on disk: {fontFilename}");
        }
        
        if(!_fontTexture.IsNull) {
            // Destroy the existing texture.
            Render.DestroyTexture(_fontTexture);
        }
        
        // Set the scale quality hint appropriately.
        SdlHintHelper.SetTextureFilteringMode(TextureFiltering);
        
        // Load the font as a surface.
        var loadedSurface = SdlImage.Load(fontFilename);
        if(loadedSurface.IsNull) {
            var sdlError = Error.GetError();
            throw new SdlImageLoadException(sdlError);
        }
        
        // Convert the surface to RGBA32.
        var rgba32Surface = Surface.ConvertSurfaceFormat(loadedSurface, GetRgba32TextureFormat());
        if(rgba32Surface.IsNull) {
            var sdlError = Error.GetError();
            throw new SdlFormatConversionException(sdlError);
        }

        // Free the original surface.
        Surface.FreeSurface(loadedSurface);

        // Create the hardware accelerated texture.
        SdlHintHelper.SetTextureFilteringMode(TextureFiltering);
        _fontTexture = Render.CreateTextureFromSurface(SdlRenderer, rgba32Surface);
        if(_fontTexture.IsNull) {
            var sdlError = Error.GetError();
            throw new SdlTextureInitializationException(sdlError);
        }

        // Free the RGBA32 surface.
        Surface.FreeSurface(rgba32Surface);

        // Calculate the glyphs per row, total number of glyphs, etc.
        _fontGlyphWidth = glyphWidth;
        _fontGlyphHeight = glyphHeight;

        _ = Render.QueryTexture(_fontTexture, out _, out _, out var textureWidth, out var textureHeight);
        _fontGlyphsPerRow = textureWidth / _fontGlyphWidth;
        _fontGlyphCount = textureHeight / _fontGlyphHeight * _fontGlyphsPerRow;
        
        // Recalculate all computed values.
        RecalculateSizesAndBounds();
    }

    /// <summary>
    /// Recalculates all computed values, such as <see cref="ConsoleWidth"/> and <see cref="ConsoleHeight"/>. This
    /// should be called whenever the font is changed or whenever padding values are changed. This will cause the next
    /// <see cref="Update"/> call to completely redraw the console.
    /// </summary>
    private void RecalculateSizesAndBounds() {
        // Update everything.
        _updatePadding = true;
        _updateTextArea = true;
        
        // Calculate total glyph size.
        _glyphTotalWidth = _fontGlyphWidth + GlyphPadding.Left + GlyphPadding.Right;
        _glyphTotalHeight = _fontGlyphHeight + GlyphPadding.Top + GlyphPadding.Bottom;

        // Calculate total text area.
        _textAreaWidth = Width - ConsolePadding.Left - ConsolePadding.Right;
        _textAreaHeight = Height - ConsolePadding.Top - ConsolePadding.Bottom;
        
        // Calculate the number of glyphs that can fit within the text area.
        ConsoleWidth = _textAreaWidth / _glyphTotalWidth;
        ConsoleHeight = _textAreaHeight / _glyphTotalHeight;
        _consoleTotalGlyphs = ConsoleWidth * ConsoleHeight;
        
        // Readjust the total text area based on the number of glyphs we can actually draw.
        _textAreaWidth = ConsoleWidth * _glyphTotalWidth;
        _textAreaHeight = ConsoleHeight * _glyphTotalHeight;
        
        // Only resize the buffer array if necessary.
        if(_consoleBuffer.Length == _consoleTotalGlyphs) return;
        
        var newBuffer = new Glyph[_consoleTotalGlyphs];
        var glyphsToCopy = _consoleTotalGlyphs < _consoleBuffer.Length ? _consoleTotalGlyphs : _consoleBuffer.Length;
        Array.Copy(_consoleBuffer, newBuffer, glyphsToCopy);
        _consoleBuffer = newBuffer;
        
        // Pad the remainder of the array (if applicable) with DefaultGlyph.
        if(_consoleTotalGlyphs <= glyphsToCopy) return;

        var glyphsToFill = _consoleBuffer.Length - glyphsToCopy - 1;
        Array.Fill(_consoleBuffer, DefaultGlyph, glyphsToCopy, glyphsToFill);
    }

    /// <summary>
    /// Sets the cursor to a particular position on the console.
    /// </summary>
    /// <param name="position">An <see cref="SdlPoint"/> containing the target coordinates. Note that both coordinates
    /// are zero-index values.</param>
    /// <exception cref="OverflowException">Thrown if the requested coordinates exceed the bounds of the console.</exception>
    public void SetCursorPosition(SdlPoint position) => SetCursorPosition(position.X, position.Y);

    /// <summary>
    /// Sets the cursor to a particular position on the console.
    /// </summary>
    /// <param name="x">The X position to set the cursor to. Note that this is a zero-indexed value.</param>
    /// <param name="y">The Y position to set the cursor to. Note that this is a zero-indexed value.</param>
    /// <exception cref="OverflowException">Thrown if the requested coordinates exceed the bounds of the console.</exception>
    public void SetCursorPosition(int x, int y) {
        if((x < 0 || x >= ConsoleWidth) || (y < 0 || y >= ConsoleHeight)) {
            throw new OverflowException("SdlKitConsole: Attempted to set the console cursor to an invalid value. " +
                                        $"Position requested: ({x}, {y}); possible locations: (0, 0)-({ConsoleWidth - 1}, {ConsoleHeight - 1})");
        }

        _cursorPosition = y * ConsoleWidth + x;
    }

    /// <summary>
    /// Scrolls the console.
    /// </summary>
    /// <param name="lines">The number of lines to scroll the console.</param>
    public void Scroll(int lines = 1) {
        if(lines == 0) {
            // >:|
            return;
        }
        
        if(lines >= ConsoleHeight || lines <= -ConsoleHeight) {
            Clear();
            return;
        }
        
        // Perform the copy operation.
        var absLines = Math.Abs(lines);
        var copyCount = ConsoleWidth * (ConsoleHeight - absLines);
        var sourceIndex = lines > 0 ? ConsoleWidth * absLines : 0;
        var destinationIndex = lines < 0 ? ConsoleWidth * absLines : 0;
        Array.Copy(_consoleBuffer, sourceIndex, _consoleBuffer, destinationIndex, copyCount);
        
        // Fill in the blank part of the array.
        sourceIndex = lines > 0 ? ConsoleWidth * (ConsoleHeight - absLines) : 0;
        var fillCount = absLines * ConsoleWidth;
        Array.Fill(_consoleBuffer, DefaultGlyph, sourceIndex, fillCount);
    }

    /// <summary>
    /// Sets the given cell to a particular glyph. This method does not move the console cursor.
    /// </summary>
    /// <param name="x">The X coordinate of the cell to set.</param>
    /// <param name="y">The Y coordinate of the cell to set.</param>
    /// <param name="glyph">The <see cref="Glyph"/> to set the cell to.</param>
    /// <exception cref="OverflowException">Thrown if the requested coordinates exceed the bounds of the console.</exception>
    public void SetCell(int x, int y, Glyph glyph) =>
        SetCell(x, y, glyph.GlyphIndex, glyph.ForegroundColor, glyph.BackgroundColor);

    /// <summary>
    /// Sets the given cell to a particular glyph. This method does not move the console cursor.
    /// </summary>
    /// <param name="x">The X coordinate of the cell to set.</param>
    /// <param name="y">The Y coordinate of the cell to set.</param>
    /// <param name="glyphIndex">The index of the character that should be placed in the cell.</param>
    /// <param name="foregroundColor">The color to set the foreground of the cell to. If this is <c>null</c>, the
    /// existing value will be used.</param>
    /// <param name="backgroundColor">The color to set the background of the cell to. If this is <c>null</c>, the
    /// existing value will be used.</param>
    /// <exception cref="OverflowException">Thrown if the requested coordinates exceed the bounds of the console or if
    /// too high of a glyph number is being referenced.</exception>
    public void SetCell(int x, int y, int glyphIndex, SdlColor? foregroundColor, SdlColor? backgroundColor) {
        if((x < 0 || x >= ConsoleWidth) || (y < 0 || y >= ConsoleHeight)) {
            throw new OverflowException("SdlKitConsole: Attempted to set the console cursor to an invalid value. " +
                                        $"Position requested: ({x}, {y}); possible locations: (0, 0)-({ConsoleWidth - 1}, {ConsoleHeight - 1})");
        }

        if(glyphIndex >= _consoleTotalGlyphs) {
            throw new OverflowException($"SdlKitConsole: Attempted to use glyph index {glyphIndex} (maximum " +
                                        $"detected index is {_fontGlyphCount}).");
        }

        var index = y * ConsoleWidth + x;
        SetCell(index, glyphIndex, foregroundColor, backgroundColor);
    }

    /// <summary>
    /// Sets the given cell to a particular glyph. This method does not move the console cursor. This method also does
    /// not perform any bounds checking and is intended for internal use only.
    /// </summary>
    /// <param name="index">The index of the console buffer to update.</param>
    /// <param name="glyphIndex">The index of the character that should be placed in the cell.</param>
    /// <param name="foregroundColor">The color to set the foreground of the cell to. If this is <c>null</c>, the
    /// existing value will be used.</param>
    /// <param name="backgroundColor">The color to set the background of the cell to. If this is <c>null</c>, the
    /// existing value will be used.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetCell(int index, int glyphIndex, SdlColor? foregroundColor = null, SdlColor? backgroundColor = null) {
        _consoleBuffer[index].GlyphIndex = glyphIndex;
        
        if(foregroundColor is not null) {
            _consoleBuffer[index].ForegroundColor = foregroundColor.Value;
        }
        
        if(backgroundColor is not null) {
            _consoleBuffer[index].BackgroundColor = backgroundColor.Value;
        }

        _updateTextArea = true;
    }

    /// <summary>
    /// Sets the cell at the cursor's position to a particular glyph. This method does not move the console cursor.
    /// </summary>
    /// <param name="glyph">The <see cref="Glyph"/> to set the cell to.</param>
    public void SetCellAtCursor(Glyph glyph) =>
        SetCellAtCursor(glyph.GlyphIndex, glyph.ForegroundColor, glyph.BackgroundColor);
    
    /// <summary>
    /// Sets the cell at the cursor's position to a particular glyph. This method does not move the console cursor.
    /// </summary>
    /// <param name="glyphIndex">The index of the character that should be placed in the cell.</param>
    /// <param name="foregroundColor">The color to set the foreground of the cell to. If this is <c>null</c>, the
    /// existing value will be used.</param>
    /// <param name="backgroundColor">The color to set the background of the cell to. If this is <c>null</c>, the
    /// existing value will be used.</param>
    public void SetCellAtCursor(int glyphIndex, SdlColor? foregroundColor = null, SdlColor? backgroundColor = null) {
        _consoleBuffer[_cursorPosition].GlyphIndex = glyphIndex;
        
        if(foregroundColor is not null) {
            _consoleBuffer[_cursorPosition].ForegroundColor = foregroundColor.Value;
        }
        
        if(backgroundColor is not null) {
            _consoleBuffer[_cursorPosition].BackgroundColor = backgroundColor.Value;
        }

        _updateTextArea = true;
    }

    /// <summary>
    /// Updates this <see cref="TextConsole"/>.
    /// </summary>
    internal override void Update() {
        var paddingColor = PaddingColor ?? DefaultGlyph.BackgroundColor;
        
        if(SdlTexture.IsNull || _fontTexture.IsNull) {
            return;
        }

        if(_updatePadding || _updateTextArea) {
            _ = Render.SetRenderTarget(SdlRenderer, SdlTexture);
        }

        if(_updatePadding) {
            _updatePadding = false;
            var paddingRight = Width - _textAreaWidth - _consolePadding.Left;
            var paddingBottom = Height - _textAreaHeight - _consolePadding.Top;
            
            var rects = new List<SdlRect>();
            
            // Top
            if(_consolePadding.Top > 0) {
                rects.Add(new SdlRect(0, 0, Width, _consolePadding.Top));
            }
            
            // Bottom
            if(paddingBottom > 0) {
                rects.Add(new SdlRect(0, Height - paddingBottom, Width, paddingBottom));
            }
            
            // Left
            if(_consolePadding.Left > 0) {
                rects.Add(new SdlRect(0, 0, _consolePadding.Left, Height));
            }
            
            // Right
            if(paddingRight > 0) {
                rects.Add(new SdlRect(Width - paddingRight, 0, paddingRight, Height));
            }
            
            // If any borders need updated, do it here.
            if(rects.Count > 0) {
                _ = Render.SetRenderDrawColor(SdlRenderer, paddingColor.R, paddingColor.G, paddingColor.B, paddingColor.A);
                _ = Render.RenderFillRects(SdlRenderer, rects.ToArray(), rects.Count);
            }
        }

        if(_updateTextArea) {
            _updateTextArea = false;

            var backgroundRect = new SdlRect(
                ConsolePadding.Left, ConsolePadding.Top,
                _glyphTotalWidth, _glyphTotalHeight
            );
            var fontSourceRect = new SdlRect(0, 0, _fontGlyphWidth, _fontGlyphHeight);
            var foregroundRect = new SdlRect(
                backgroundRect.X + GlyphPadding.Left, backgroundRect.Y + GlyphPadding.Top,
                _fontGlyphWidth, _fontGlyphHeight
            );

            for(var y = 0; y < ConsoleHeight; y++) {
                for(var x = 0; x < ConsoleWidth; x++) {
                    var glyph = _consoleBuffer[y * ConsoleWidth + x];

                    fontSourceRect.X = glyph.GlyphIndex % _fontGlyphsPerRow * _fontGlyphWidth;
                    fontSourceRect.Y = glyph.GlyphIndex / _fontGlyphsPerRow * _fontGlyphHeight;
                    
                    _ = Render.SetRenderDrawColor(SdlRenderer, glyph.BackgroundColor.R, glyph.BackgroundColor.G, glyph.BackgroundColor.B, glyph.BackgroundColor.A);
                    _ = Render.RenderFillRect(SdlRenderer, ref backgroundRect);
                    
                    _ = Render.SetTextureColorMod(_fontTexture, glyph.ForegroundColor.R, glyph.ForegroundColor.G, glyph.ForegroundColor.B);
                    _ = Render.SetTextureAlphaMod(_fontTexture, glyph.ForegroundColor.A);
                    _ = Render.RenderCopy(SdlRenderer, _fontTexture, ref fontSourceRect, ref foregroundRect);

                    backgroundRect.X += _glyphTotalWidth;
                    foregroundRect.X += _glyphTotalWidth;
                }

                backgroundRect.X = ConsolePadding.Left;
                foregroundRect.X = backgroundRect.X + GlyphPadding.Left;
                
                backgroundRect.Y += _glyphTotalHeight;
                foregroundRect.Y += _glyphTotalHeight;
            }
        }
        
        if(_updatePadding || _updateTextArea) {
            _ = Render.SetRenderDrawColor(SdlRenderer, 0, 0, 0, 255);
        }
    }

    /// <summary>
    /// Prints a character to the screen at the current cursor position and advances the cursor or passes a control
    /// character to the console.
    /// </summary>
    /// <param name="character">Prints the given character or interprets a control character. The following control
    /// characters are supported:
    /// <list type="bullet">
    ///     <item>
    ///         <description><c>0x08</c> (<c>\b</c>) - Backspace. Moves the cursor back one position and deletes the
    /// previous character. If the cursor is already at the beginning of the line this will do nothing.</description>
    ///     </item>
    ///     <item>
    ///         <description><c>0x0A</c> (<c>\n</c>) - Line feed. Advances the cursor to the next line and moves the
    /// cursor to the beginning of the line. If the end of the buffer is reached, it will automatically be scrolled
    /// up.</description>
    ///     </item>
    ///     <item>
    ///         <description><c>0x0D</c> (<c>\b</c>) - Carriage return. Moves the cursor to the beginning of the current
    /// line.</description>
    ///     </item>
    /// </list></param>
    /// <param name="foregroundColor">The foreground color of the written character. If this is set to <c>null</c>, the
    /// cell's current color values will be used. If a control character is passed, this will be ignored.</param>
    /// <param name="backgroundColor">The background color of the written character. If this is set to <c>null</c>, the
    /// cell's current color values will be used. If a control character is passed, this will be ignored.</param>
    /// <param name="ignoreControlCharacters">If this is set to <c>true</c>, control characters will not be parsed. If
    /// this is set to <c>false</c> they will be. This defaults to <c>false</c>.</param>
    public void Write(int character, SdlColor? foregroundColor = null, SdlColor? backgroundColor = null, bool ignoreControlCharacters = false) {
        var (cursorX, cursorY) = GetCursorPosition();
        
        if(character is 0x08 or 0x0A or 0x0D && !ignoreControlCharacters) {
            // Interpret control character.
            switch(character) {
                case 0x08:
                    if(cursorX == 0) return;
                    SetCursorPosition(--cursorX, cursorY);
                    SetCellAtCursor(DefaultGlyph);
                    break;
                
                case 0x0A:
                    if(cursorY >= ConsoleHeight - 1) {
                        // The cursor is at the bottom of the console. Scroll or wrap, depending on settings.
                        if(AutoScroll) {
                            Scroll();
                            SetCursorPosition(0, cursorY);  // Return cursor to the beginning of the line.
                        } else {
                            _cursorPosition = 0;  // Return cursor to the upper-left.
                        }
                        return;
                    }
                    
                    // The cursor is NOT at the bottom of the screen. Yay.
                    SetCursorPosition(0, ++cursorY);
                    break;
                
                case 0x0D:
                    SetCursorPosition(0, cursorY);
                    break;
            }
            
            return;
        }
        
        // Print the character and advance the cursor.
        SetCellAtCursor(character, foregroundColor, backgroundColor);

        if(++_cursorPosition >= _consoleTotalGlyphs) {
            // Do we scroll or wrap?
            if(AutoScroll) {
                Scroll();
                SetCursorPosition(0, ConsoleHeight - 1);
            } else {
                _cursorPosition = 0;
            }
        }
    }

    /// <summary>
    /// Prints a string to the console.
    /// </summary>
    /// <param name="str">The string to print to the console.</param>
    /// <param name="foregroundColor">The foreground color of the written string. If this is set to <c>null</c>, each
    /// string character will use the default values of their respective cell.</param>
    /// <param name="backgroundColor">The background color of the written string. If this is set to <c>null</c>, each
    /// string character will use the default values of their respective cell.</param>
    /// <param name="ignoreControlCharacters">If this is set to <c>true</c>, control characters within
    /// <paramref name="str"/>will not be parsed. If this is set to <c>false</c> they will be. This defaults to
    /// <c>false</c>.</param>
    public void Write(string str, SdlColor? foregroundColor = null, SdlColor? backgroundColor = null, bool ignoreControlCharacters = false) {
        foreach(var ch in str) {
            Write(ch, foregroundColor, backgroundColor, ignoreControlCharacters);
        }
    }

    /// <summary>
    /// Prints a character to the screen at the current cursor position and advances the cursor. Note that control
    /// characters will never be interpreted with this method.
    /// </summary>
    /// <param name="glyph">The glyph to print.</param>
    public void Write(Glyph glyph) => Write(glyph.GlyphIndex, glyph.ForegroundColor, glyph.BackgroundColor, true);

    /// <summary>
    /// Prints a string and newline to the console.
    /// </summary>
    /// <param name="str">The string to print to the console.</param>
    /// <param name="foregroundColor">The foreground color of the written string. If this is set to <c>null</c>, each
    /// string character will use the default values of their respective cell.</param>
    /// <param name="backgroundColor">The background color of the written string. If this is set to <c>null</c>, each
    /// string character will use the default values of their respective cell.</param>
    /// <param name="ignoreControlCharacters">If this is set to <c>true</c>, control characters within
    /// <paramref name="str"/>will not be parsed. If this is set to <c>false</c> they will be. This defaults to
    /// <c>false</c>.</param>
    public void WriteLine(string str, SdlColor? foregroundColor = null, SdlColor? backgroundColor = null, bool ignoreControlCharacters = false) {
        Write(str, foregroundColor, backgroundColor, ignoreControlCharacters);
        Write('\n');
    }
}
