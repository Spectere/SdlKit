namespace Spectere.SdlKit.Renderables;

public partial class TileGrid {
    /// <summary>
    /// Represents a tile grid layer. This is used to store tile data, as well as properties related to the layer as
    /// a whole.
    /// </summary>
    private protected class Layer {
        /// <summary>
        /// Stores the tile data for this <see cref="Layer"/>
        /// </summary>
        internal int[] Tiles;

        /// <summary>
        /// Gets or sets the alpha modulation value that will be multiplied into all rendering operations. This will cause
        /// the texture to appear translucent when rendered, with higher values appearing more opaque. This defaults
        /// to 255.
        /// </summary>
        internal byte AlphaModulation { get; set; } = 255;

        /// <summary>
        /// The blending mode used for this <see cref="IRenderable"/>. This defaults to <see cref="BlendMode.Alpha"/>.
        /// </summary>
        internal BlendMode BlendMode { get; set; } = BlendMode.Alpha;

        /// <summary>
        /// Gets or sets an additional color value multiplied into all rendering operations. This will cause the texture
        /// to be tinted by the given <see cref="SdlColor"/> when the texture is copied. Note that the alpha channel of the
        /// <see cref="SdlColor"/> will be ignored by this operation. This defaults to white (R: 255, G: 255, B: 255).
        /// </summary>
        internal SdlColor ColorModulation { get; set; } = new(255, 255, 255);

        /// <summary>
        /// Gets or sets whether the layer should be drawn. This defaults to <c>true</c>.
        /// </summary>
        internal bool Visible { get; set; } = true;
        
        /// <summary>
        /// Initializes a new <see cref="Layer"/>.
        /// </summary>
        /// <param name="size">The size of the layer, in tiles.</param>
        internal Layer(int size) {
            Tiles = new int[size];
        }

        /// <summary>
        /// Sets the given tile to a particular value.
        /// </summary>
        /// <param name="index">The index of the tile to set. If an out of bounds value is given, an exception will
        /// be thrown.</param>
        /// <param name="value">The value to set the tile to. If this is a negative value, the tile will be blank and
        /// fully transparent. If it is 0 or above, a tile from the tile sheet associated with the underlying
        /// <see cref="TileGrid"/> will be used.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the value in <paramref name="index"/> is outside of the
        /// valid range.</exception>
        internal void SetTile(int value, int index) => Tiles[index] = value;

        /// <summary>
        /// Sets many tiles at once.
        /// </summary>
        /// <param name="value">The value to set the tiles to. If this is a negative value, the tiles will be blank and
        /// fully transparent. If it is 0 or above, a tile from the tile sheet associated with the underlying
        /// <see cref="TileGrid"/> will be used.</param>
        /// <param name="startingIndex">The index where the fill should begin.</param>
        /// <param name="length">The number of tiles that should be filled. If this is set to 0 or below, the remainder
        /// of the layer will be filled in.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the value in <paramref name="startingIndex"/> is
        /// outside of the valid range.</exception>
        internal void SetTiles(int value, int startingIndex, int length) {
            var count = length <= 0 || startingIndex + length >= Tiles.Length
                ? Tiles.Length - startingIndex
                : length
                ;
            
            Array.Fill(Tiles, value, startingIndex, count);
        }
        
        /// <summary>
        /// Sets many tiles at once.
        /// </summary>
        /// <param name="values">The values to set the tiles to. If any negative values are used, those tiles will be
        /// blank and fully transparent. If the values are 0 or above, a tile from the tile sheet associated with the
        /// underlying <see cref="TileGrid"/> will be used.</param>
        /// <param name="repeat">If this is set to <c>true</c> and the total fill length exceeds the number of tiles in
        /// <paramref name="values"/>, the desired tiles will be repeated until the entire length has been filled or
        /// the end of the tile array has been reached, whichever comes first.</param>
        /// <param name="startingIndex">The index where the fill should begin.</param>
        /// <param name="length">The number of tiles that should be filled. If this is less than the number of tiles in
        /// <paramref name="values"/>, only that number of values will be filled in. If this is greater than the number
        /// of tiles in the values array, or 0 and below, the exact behavior will depend on the value of the
        /// <paramref name="repeat"/> parameter. If this is 0 or below, it implies that all tiles from
        /// <paramref name="startingIndex"/> and beyond should be considered.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the value in <paramref name="startingIndex"/> is
        /// outside of the valid range.</exception>
        internal void SetTiles(int[] values, int startingIndex, int length, bool repeat = false) {
            if(values.Length == 0) {
                // But why, though? :|
                return;
            }
    
            if(values.Length == 1) {
                if(repeat == false) {
                    SetTile(startingIndex, values[0]);
                } else {
                    SetTiles(startingIndex, length, values[0]);
                }
                return;
            }
    
            if(!repeat) {
                var copyLength = length <= 0
                    ? values.Length
                    : length < values.Length
                        ? length
                        : values.Length
                    ;
        
                copyLength = copyLength > Tiles.Length - startingIndex
                    ? Tiles.Length - startingIndex
                    : copyLength
                    ;

                Array.Copy(values, 0, Tiles, startingIndex, copyLength);
                return;
            }

            var spanLength = length <= 0 || length > Tiles.Length - startingIndex
                ? Tiles.Length - startingIndex
                : length
                ;

            var span = new Span<int>(Tiles, startingIndex, spanLength);
            for(var i = 0; i < span.Length; i++) {
                span[i] = values[i % values.Length];
            }
        }
    }
}
