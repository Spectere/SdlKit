namespace Spectere.SdlKit;

/// <summary>
/// Defines a contract for an object that can be drawn using an SDL renderer.
/// </summary>
public interface IRenderable : IComparable<IRenderable> {
    /// <summary>
    /// The blending mode used for this <see cref="IRenderable"/>.
    /// </summary>
    public BlendMode BlendMode { get; set; }
    
    /// <summary>
    /// The screen-space coordinates where this <see cref="IRenderable"/> should be drawn. The
    /// <see cref="SdlRect.Position"/> field corresponds to the upper-left corner of where this will be drawn to the
    /// screen, and the <see cref="SdlRect.Size"/> indicates the size in which this will be drawn. If this is larger
    /// or smaller than the backing texture, the resulting image will be scaled accordingly. 
    /// </summary>
    public SdlRect? Destination { get; }
    
    /// <summary>
    /// If <c>true</c>, this <see cref="IRenderable"/> is flipped horizontally. Otherwise, it is drawn normally.
    /// </summary>
    public bool FlipHorizontal { get; set; }

    /// <summary>
    /// If <c>true</c>, this <see cref="IRenderable"/> is flipped vertically. Otherwise, it is drawn normally.
    /// </summary>
    public bool FlipVertical { get; set; }
    
    /// <summary>
    /// Gets the height of the backing texture.
    /// </summary>
    public int Height { get; }
    
    /// <summary>
    /// The angle at which to rotate this <see cref="IRenderable"/>.
    /// </summary>
    public double RotationAngle { get; set; }

    /// <summary>
    /// The center point at which to rotate this <see cref="IRenderable"/>. This is only used if <see cref="RotationAngle"/>
    /// is specified. If this is set to <c>null</c>, this object will be rendered around its center point.
    /// </summary>
    public SdlPoint? RotationCenter { get; }
    
    /// <summary>
    /// Gets the filtering mode that is used when scaling this texture.
    /// </summary>
    public TextureFiltering TextureFiltering { get; }
    
    /// <summary>
    /// Gets or sets whether or not this <see cref="IRenderable"/> will be copied to its parent <see cref="RenderTarget"/>.
    /// </summary>
    public bool Visible { get; set; }
    
    /// <summary>
    /// Gets the width of the backing texture.
    /// </summary>
    public int Width { get; }
    
    /// <summary>
    /// Specifies a <see cref="SdlRect"/> within this <see cref="IRenderable"/>. If this is specified, only the pixels
    /// contained within this rectangle will be copied to the render target. If this is <c>null</c>, the entire texture
    /// will be copied. 
    /// </summary>
    public SdlRect? Window { get; }
    
    /// <summary>
    /// Sets which layer in which this <see cref="IRenderable"/> should be drawn, relative to its associated
    /// <see cref="RenderTarget"/>. Layers will be drawn in order from lowest to highest, thus causing the higher
    /// valued layers to appear on top. 
    /// </summary>
    public int ZOrder { get; set; }

    /// <summary>
    /// Resizes this <see cref="IRenderable"/>. Note that the contents of this object will very likely have to be
    /// redrawn manually.
    /// </summary>
    /// <param name="newWidth">The new width of this <see cref="IRenderable"/>.</param>
    /// <param name="newHeight">The new height of this <see cref="IRenderable"/>.</param>
    public void Resize(int newWidth, int newHeight);
}
