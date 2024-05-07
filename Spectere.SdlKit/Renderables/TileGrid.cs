using Spectere.SdlKit.Exceptions;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.Render;
using System.Runtime.CompilerServices;

namespace Spectere.SdlKit.Renderables;

/// <summary>
/// Defines an SDL tile grid. This cannot be drawn to directly and is instead made up of a set of tiles from a given
/// tile sheet.
/// </summary>
public partial class TileGrid : Renderable {
    /// <summary>
    /// If this is set to <c>true</c>, the <see cref="TileGrid"/> will be re-composited and redrawn when
    /// <see cref="Update"/> is called.
    /// </summary>
    private bool _redrawTileGrid = true;

    /// <summary>
    /// The list of layers contained in this <see cref="TileGrid"/>.
    /// </summary>
    private readonly List<Layer> _layers = new();

    /// <summary>
    /// An SDL texture that contains the tile sheet.
    /// </summary>
    private SdlTexture _tileSheetTexture;

    /// <summary>
    /// The number of tiles contained in each row of the tile sheet.
    /// </summary>
    private int _tilesPerRowInSheet;

    /// <summary>
    /// Gets or sets the background color of this <see cref="TileGrid"/>. This will be drawn in all places where a tile
    /// is not placed and can also be seen through translucent pixels.
    /// </summary>
    public SdlColor BackgroundColor {
        get => _backgroundColor;
        set {
            _backgroundColor = value;
            _redrawTileGrid = true;
        }
    }
    private SdlColor _backgroundColor;
    
    /// <summary>
    /// The width of this <see cref="TileGrid"/>, in tiles.
    /// </summary>
    public int GridHeight { get; }

    /// <summary>
    /// The width of this <see cref="TileGrid"/>, in tiles.
    /// </summary>
    public int GridWidth { get; }
    
    /// <summary>
    /// The width of each tile.
    /// </summary>
    public int TileHeight { get; }
    
    /// <summary>
    /// The height of each tile.
    /// </summary>
    public int TileWidth { get; }
    
    /// <summary>
    /// The number of tiles that are defined in the loaded tile sheet.
    /// </summary>
    public int TotalTilesInSheet { get; private set; }

    /// <summary>
    /// Creates a new <see cref="TileGrid"/>. The resulting grid will have a single layer, with index 0.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> that should be used to create the backing texture.</param>
    /// <param name="gridWidth">The width of the <see cref="TileGrid"/>, in tiles.</param>
    /// <param name="gridHeight">The height of the <see cref="TileGrid"/>, in tiles.</param>
    /// <param name="tileSheetFilename">The name of the tile sheet that should be initially used by this <see cref="TileGrid"/>.</param>
    /// <param name="tileWidth">The width of each tile in the tile sheet, in pixels.</param>
    /// <param name="tileHeight">The height of each tile in the tile sheet, in pixels.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="TileGrid"/> should use.</param>
    /// <exception cref="SdlTextureInitializationException">Thrown when SDL is unable to create a texture.</exception>
    internal TileGrid(SdlRenderer renderer, int gridWidth, int gridHeight, string tileSheetFilename, int tileWidth, int tileHeight, TextureFiltering textureFiltering = TextureFiltering.Nearest)
        : base(renderer, TextureAccess.Target, gridWidth * tileWidth, gridHeight * tileHeight, textureFiltering) {
        GridHeight = gridHeight;
        GridWidth = gridWidth;

        TileHeight = tileHeight;
        TileWidth = tileWidth;

        LoadTileSheet(tileSheetFilename);
        AddLayer();
    }
    
    /// <summary>
    /// Creates a new <see cref="TileGrid"/>. The resulting grid will have a single layer, with index 0.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> whose renderer should be used to create the backing texture.</param>
    /// <param name="gridWidth">The width of the <see cref="TileGrid"/>, in tiles.</param>
    /// <param name="gridHeight">The height of the <see cref="TileGrid"/>, in tiles.</param>
    /// <param name="tileSheetFilename">The name of the tile sheet that should be initially used by this <see cref="TileGrid"/>.</param>
    /// <param name="tileWidth">The width of each tile in the tile sheet, in pixels.</param>
    /// <param name="tileHeight">The height of each tile in the tile sheet, in pixels.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="TileGrid"/> should use.</param>
    /// <exception cref="SdlTextureInitializationException">Thrown when SDL is unable to create a texture.</exception>
    public TileGrid(Window window, int gridWidth, int gridHeight, string tileSheetFilename, int tileWidth, int tileHeight, TextureFiltering textureFiltering = TextureFiltering.Nearest)
        : this(window.SdlRenderer, gridWidth, gridHeight, tileSheetFilename, tileWidth, tileHeight, textureFiltering) { }

    /// <summary>
    /// Creates a new blank layer.
    /// </summary>
    /// <returns>The numeric index of the new layer.</returns>
    public int AddLayer() {
        var newIndex = _layers.Count;
        _layers.Add(new Layer(GridWidth * GridHeight));
        return newIndex;
    }

    /// <summary>
    /// Creates a new layer with a defined set of tiles.
    /// </summary>
    /// <param name="tiles">An array containing the tiles to prepopulate the layer with. If the array is larger than
    /// the total number of tiles in the <see cref="TileGrid"/>, it will be truncated. If the array is smaller than
    /// the number of tiles in the grid, the behavior depends on the <paramref name="repeat"/> parameter.</param>
    /// <param name="repeat">If this is <c>true</c> and the <paramref name="tiles"/> array is shorter than the number of
    /// tiles in this <see cref="TileGrid"/>, the tiles will be repeated. If this is <c>false</c>, the new layer will
    /// only have the tiles defined in <paramref name="tiles"/> and no more. This defaults to <c>false</c>.</param>
    /// <returns>The numeric index of the new layer.</returns>
    public int AddLayer(int[] tiles, bool repeat = false) {
        var layerIndex = AddLayer();
        SetTiles(layerIndex, tiles, repeat);
        return layerIndex;
    }

    /// <summary>
    /// Performs a bounds check on the given array index and throws an exception if the value is out of range.
    /// </summary>
    /// <param name="index">The index number to check.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void BoundsCheckLayer(int index) {
        if(index >= _layers.Count || index < 0) {
            throw new IndexOutOfRangeException($"TileGrid: Layer {index} is out of bounds. Only {_layers.Count} layers are present.");
        }
    }

    /// <summary>
    /// Deletes the top-most layer. Since this <see cref="Renderable"/> requires at least one layer to be present,
    /// deleting the last layer will automatically create a new, blank layer.
    /// </summary>
    public void DeleteLayer() => DeleteLayer(_layers.Count - 1);

    /// <summary>
    /// Deletes a specific layer from the layer stack. Since this <see cref="Renderable"/> requires at least one layer
    /// to be present, deleting the last layer will automatically create a new, blank layer.
    /// </summary>
    /// <param name="layer">The numeric index of the layer to delete.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if the </exception>
    public void DeleteLayer(int layer) {
        BoundsCheckLayer(layer);

        _layers.RemoveAt(layer);
        _redrawTileGrid = true;

        if(_layers.Count == 0) {
            AddLayer();
        }
    }

    /// <summary>
    /// Loads a new tile sheet. Note that the tile size cannot be changed after the <see cref="TileGrid"/> is created,
    /// and so every subsequently loaded file will be expected to have the same tile size. If the grid contains tiles
    /// that are not in the new file (for example, if a layer references tile number 451 and the new file only has
    /// 200 tiles) an exception will be thrown.
    /// </summary>
    /// <param name="tileSheetFilename">The path of the file to open.</param>
    /// <exception cref="FileNotFoundException">Thrown when the file passed in via <paramref name="tileSheetFilename"/>
    /// does not exist.</exception>
    public void LoadTileSheet(string tileSheetFilename) {
        var textureInfo = TextureHelper.LoadTextureFromFile(ref SdlRenderer, ref _tileSheetTexture, tileSheetFilename, TextureFiltering);
        
        _redrawTileGrid = true;
        
        // Recalculate number of tiles.
        _tilesPerRowInSheet = textureInfo.TextureWidth / TileWidth;
        TotalTilesInSheet = _tilesPerRowInSheet * (textureInfo.TextureHeight / TileHeight);
    }

    /// <summary>
    /// Sets the alpha modulation for a particular layer. If you wish the set the alpha modulation for the entire
    /// <see cref="TileGrid"/>, use the <see cref="Renderable.AlphaModulation"/> property instead.
    /// </summary>
    /// <param name="layer">The layer index to change.</param>
    /// <param name="alpha">The desired alpha modulation value that this layer should have. Lower values will cause it
    /// to appear more translucent, with 0 being completely transparent, while higher values will cause it to look more
    /// opaque, with 255 being completely opaque.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    public void SetLayerAlphaModulation(int layer, byte alpha) {
        BoundsCheckLayer(layer);

        if(_layers[layer].AlphaModulation == alpha) {
            return;
        }

        _layers[layer].AlphaModulation = alpha;
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Sets the blending mode for a particular layer. If you wish to set the blending mode for the entire
    /// <see cref="TileGrid"/>, use the <see cref="Renderable.BlendMode"/> property instead.
    /// </summary>
    /// <param name="layer">The layer index to change.</param>
    /// <param name="blendMode">The blend mode that should be used when rendering that layer.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    public void SetLayerBlendMode(int layer, BlendMode blendMode) {
        BoundsCheckLayer(layer);

        if(_layers[layer].BlendMode == blendMode) {
            return;
        }

        _layers[layer].BlendMode = blendMode;
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Sets the color modulation value for a particular layer. This will cause it to appear tinted. If you wish to set
    /// the color modulation for the entire <see cref="TileGrid"/>, use the <see cref="Renderable.ColorModulation"/>
    /// property instead.
    /// </summary>
    /// <param name="layer">The layer index to change.</param>
    /// <param name="color">An <see cref="SdlColor"/> representing the color to be used for the operation. Note that the
    /// alpha channel will be ignored for this operation.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    public void SetLayerColorModulation(int layer, SdlColor color) {
        BoundsCheckLayer(layer);
        
        if(_layers[layer].ColorModulation == color) {
            return;
        }

        _layers[layer].ColorModulation = color;
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Sets the visibility property for a particular layer. If you wish the set the visibility property for the entire
    /// <see cref="TileGrid"/>, use the <see cref="Renderable.Visible"/> property instead.
    /// </summary>
    /// <param name="layer">The layer index to change.</param>
    /// <param name="visible">The requested layer visibility setting.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    public void SetLayerVisible(int layer, bool visible) {
        BoundsCheckLayer(layer);
        
        if(_layers[layer].Visible == visible) {
            return;
        }

        _layers[layer].Visible = visible;
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Updates a single tile on a given layer.
    /// </summary>
    /// <param name="layer">The numeric index of the layer to update.</param>
    /// <param name="index">The tile index that should be updated on the layer.</param>
    /// <param name="value">The tile to write to the layer. If these values are negative, they will be drawn as
    /// transparent tiles. If these values are positive, an image from the tile sheet will be used. The tiles on the
    /// tile sheet are indexed sequentially, starting at 0 in the upper-left corner and incrementing from left-to-right,
    /// then top-to-bottom.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the value in <paramref name="index"/> is outside of the
    /// valid range.</exception>
    public void SetTile(int layer, int index, int value) {
        BoundsCheckLayer(layer);
        
        _layers[layer].SetTile(index, value);
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Sets all of the tiles on a given layer to a particular value.
    /// </summary>
    /// <param name="layer">The numeric index of the layer to update.</param>
    /// <param name="value">The tile to write to the layer. If these values are negative, they will be drawn as
    /// transparent tiles. If these values are positive, an image from the tile sheet will be used. The tiles on the
    /// tile sheet are indexed sequentially, starting at 0 in the upper-left corner and incrementing from left-to-right,
    /// then top-to-bottom.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    public void SetTiles(int layer, int value) {
        BoundsCheckLayer(layer);
        
        SetTiles(layer, value, 0, 0);
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Sets the tiles on a given layer to a particular value, starting with a set index and ending at the end of the
    /// tile array.
    /// </summary>
    /// <param name="layer">The numeric index of the layer to update.</param>
    /// <param name="value">The tile to write to the layer. If these values are negative, they will be drawn as
    /// transparent tiles. If these values are positive, an image from the tile sheet will be used. The tiles on the
    /// tile sheet are indexed sequentially, starting at 0 in the upper-left corner and incrementing from left-to-right,
    /// then top-to-bottom.</param>
    /// <param name="startingIndex">The index of the layer's tile array to start writing to. This is counted as the
    /// number of tiles, incremented sequentially from left-to-right, then top-to-bottom.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the value in <paramref name="startingIndex"/> is
    /// outside of the valid range.</exception>
    public void SetTiles(int layer, int value, int startingIndex) {
        BoundsCheckLayer(layer);
        
        SetTiles(layer, value, startingIndex, 0);
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Sets the tiles on a given layer to a particular value, starting with a set index and ending after a given
    /// number of tiles have been updated.
    /// </summary>
    /// <param name="layer">The numeric index of the layer to update.</param>
    /// <param name="value">The tile to write to the layer. If these values are negative, they will be drawn as
    /// transparent tiles. If these values are positive, an image from the tile sheet will be used. The tiles on the
    /// tile sheet are indexed sequentially, starting at 0 in the upper-left corner and incrementing from left-to-right,
    /// then top-to-bottom.</param>
    /// <param name="startingIndex">The index of the layer's tile array to start writing to. This is counted as the
    /// number of tiles, incremented sequentially from left-to-right, then top-to-bottom.</param>
    /// <param name="length">The maximum number of tiles to update. If this is 0 or a negative number, all tiles from
    /// the index in <paramref name="startingIndex"/> and beyond will be updated.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the value in <paramref name="startingIndex"/> is
    /// outside of the valid range.</exception>
    public void SetTiles(int layer, int value, int startingIndex, int length) {
        BoundsCheckLayer(layer);
        
        _layers[layer].SetTiles(value, startingIndex, length);
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Sets the tiles on a given layer based on an array of values, starting from the beginning of the tile array.
    /// </summary>
    /// <param name="layer">The numeric index of the layer to update.</param>
    /// <param name="values">An array of tiles values to place onto the layer. If these values are negative, they will
    /// be drawn as transparent tiles. If these values are positive, an image from the tile sheet will be used. The
    /// tiles on the tile sheet are indexed sequentially, starting at 0 in the upper-left corner and incrementing
    /// from left-to-right, then top-to-bottom.</param>
    /// <param name="repeat">If this is set to <c>true</c>, the tile values in the <paramref name="values"/> array will
    /// be repeated through the entirety of the layer's tile array.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    public void SetTiles(int layer, int[] values, bool repeat = false) {
        BoundsCheckLayer(layer);
        
        SetTiles(layer, values, 0, 0, repeat);
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Sets the tiles on a given layer based on an array of values, starting at a set point.
    /// </summary>
    /// <param name="layer">The numeric index of the layer to update.</param>
    /// <param name="values">An array of tiles values to place onto the layer. If these values are negative, they will
    /// be drawn as transparent tiles. If these values are positive, an image from the tile sheet will be used. The
    /// tiles on the tile sheet are indexed sequentially, starting at 0 in the upper-left corner and incrementing
    /// from left-to-right, then top-to-bottom.</param>
    /// <param name="startingIndex">The index of the layer's tile array to start writing to. This is counted as the
    /// number of tiles, incremented sequentially from left-to-right, then top-to-bottom.</param>
    /// <param name="repeat">If this is set to <c>true</c>, the tile values in the <paramref name="values"/> array will
    /// be repeated from the <paramref name="startingIndex"/> until the end of the layer's tile array.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the value in <paramref name="startingIndex"/> is
    /// outside of the valid range.</exception>
    public void SetTiles(int layer, int[] values, int startingIndex, bool repeat = false) {
        BoundsCheckLayer(layer);
        
        SetTiles(layer, values, startingIndex, 0, repeat);
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Sets the tiles on a given layer based on an array of values, starting at a set point and continuing along a
    /// defined length.
    /// </summary>
    /// <param name="layer">The numeric index of the layer to update.</param>
    /// <param name="values">An array of tiles values to place onto the layer. If these values are negative, they will
    /// be drawn as transparent tiles. If these values are positive, an image from the tile sheet will be used. The
    /// tiles on the tile sheet are indexed sequentially, starting at 0 in the upper-left corner and incrementing
    /// from left-to-right, then top-to-bottom.</param>
    /// <param name="startingIndex">The index of the layer's tile array to start writing to. This is counted as the
    /// number of tiles, incremented sequentially from left-to-right, then top-to-bottom.</param>
    /// <param name="length">The maximum number of tiles to update. If this is 0 or a negative number, all tiles from
    /// the index in <paramref name="startingIndex"/> and beyond can potentially be changed. If this is less than the
    /// length of the <paramref name="values"/> array, only that number of tiles will be written. If this is greater,
    /// the exact behavior of this method will depend on the <paramref name="repeat"/> parameter.</param>
    /// <param name="repeat">If this is set to <c>true</c> and <paramref name="length"/> exceeds the length of the
    /// <paramref name="values"/> array, the values will be written until <c>length</c> number of tiles have been
    /// written or the end of the array is reached, whichever comes first.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="layer"/> exceeds the number of layers in
    /// this <see cref="TileGrid"/>.</exception>
    /// <exception cref="IndexOutOfRangeException">Thrown if the value in <paramref name="startingIndex"/> is
    /// outside of the valid range.</exception>
    public void SetTiles(int layer, int[] values, int startingIndex, int length, bool repeat = false) {
        BoundsCheckLayer(layer);
        
        _layers[layer].SetTiles(values, startingIndex, length, repeat);
        _redrawTileGrid = true;
    }

    /// <summary>
    /// Updates this <see cref="TileGrid"/>.
    /// </summary>
    internal override void Update() {
        if(!_redrawTileGrid || SdlTexture.IsNull || _tileSheetTexture.IsNull) {
            return;
        }

        _redrawTileGrid = false;

        var backgroundFillRect = new SdlRect(0, 0, Width, Height);
        _ = Render.SetRenderTarget(SdlRenderer, SdlTexture);
        _ = Render.SetRenderDrawColor(SdlRenderer, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, BackgroundColor.A);
        _ = Render.RenderFillRect(SdlRenderer, ref backgroundFillRect);

        var totalTiles = GridWidth * GridHeight;
        foreach(var layer in _layers) {
            _ = Render.SetTextureAlphaMod(_tileSheetTexture, layer.AlphaModulation);
            _ = Render.SetTextureBlendMode(_tileSheetTexture, layer.BlendMode);
            _ = Render.SetTextureColorMod(_tileSheetTexture, layer.ColorModulation.R, layer.ColorModulation.G, layer.ColorModulation.B);

            for(var idx = 0; idx < totalTiles; idx++) {
                var destinationX = (idx % GridWidth) * TileWidth;
                var destinationY = (idx / GridWidth) * TileHeight;

                var tile = layer.Tiles[idx];
                if(tile >= TotalTilesInSheet) {
                    throw new IndexOutOfRangeException($"TileGrid: Tile index {tile} is out of range (only {TotalTilesInSheet} are available in the tile sheet!)");
                }

                var tileX = (tile % _tilesPerRowInSheet) * TileWidth;
                var tileY = (tile / _tilesPerRowInSheet) * TileHeight;
                var tileRect = new SdlRect(tileX, tileY, TileWidth, TileHeight);
                var destRect = new SdlRect(destinationX, destinationY, TileWidth, TileHeight);
                _ = Render.RenderCopy(SdlRenderer, _tileSheetTexture, ref tileRect, ref destRect);
            }
        }

        _ = Render.SetRenderDrawColor(SdlRenderer, 0, 0, 0, 255);
    }
}
