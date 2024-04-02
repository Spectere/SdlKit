using SdlHints = Spectere.SdlKit.Interop.Sdl.Support.Hints;
using Spectere.SdlKit.Exceptions;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.Render;

namespace Spectere.SdlKit;

/// <summary>
/// A base <see cref="IRenderable"/> implementation. This class cannot be instantiated.
/// </summary>
public abstract class Renderable : IRenderable, IDisposable {
    /// <summary>
    /// If this is set to <c>true</c>, this <see cref="Renderable"/> has a <c>null</c> texture. This is typically used
    /// with dummy rendering targets to ensure that they target the SDL window directly.
    /// </summary>
    private readonly bool _nullTexture;
    
    /// <summary>
    /// A numeric representation of the pixel format of this <see cref="Renderable"/>.
    /// </summary>
    private readonly uint _pixelFormat;
    
    /// <summary>
    /// A <see cref="TextureAccess"/> representing how this texture is meant to be accessed. This is necessary because
    /// some access methods need to be updated in a different way.
    /// </summary>
    private protected readonly TextureAccess TextureAccess;

    /// <inheritdoc/>
    public BlendMode BlendMode {
        get => _blendMode;
        set {
            _blendMode = value;
            _ = Render.SetTextureBlendMode(SdlTexture, _blendMode);
        }
    }
    private BlendMode _blendMode;

    /// <inheritdoc/>
    public SdlColor ColorModulation {
        get {
            var sdlColor = new SdlColor();
            if(_nullTexture) {
                return sdlColor;
            }
            
            _ = Render.GetTextureColorMod(SdlTexture, ref sdlColor.R, ref sdlColor.G, ref sdlColor.B);
            return sdlColor;
        }

        set {
            if(_nullTexture) {
                return;
            }

            _ = Render.SetTextureColorMod(SdlTexture, value.R, value.G, value.B);
        }
    }

    /// <inheritdoc/>
    public SdlRect? Destination {
        get => _destination;
        set => _destination = value ?? new SdlRect(0, 0, Width, Height);
    }
    private SdlRect _destination;

    /// <summary>
    /// If this is set to <c>true</c>, this object has been disposed of and must not be updated, copied, or rendered.
    /// </summary>
    public bool Disposed { get; private set; }

    /// <inheritdoc/>
    public bool FlipHorizontal {
        get => _flipDirection.HasFlag(FlipDirection.Horizontal);
        set {
            if(value) {
                _flipDirection |= FlipDirection.Horizontal;
            } else {
                _flipDirection &= ~FlipDirection.Horizontal;
            }
        }
    }

    /// <inheritdoc/>
    public bool FlipVertical {
        get => _flipDirection.HasFlag(FlipDirection.Vertical);
        set {
            if(value) {
                _flipDirection |= FlipDirection.Vertical;
            } else {
                _flipDirection &= ~FlipDirection.Vertical;
            }
        }
    }
    private FlipDirection _flipDirection = FlipDirection.None;


    /// <inheritdoc/>
    public int Height { get; private set; }

    /// <inheritdoc/>
    public double RotationAngle { get; set; }

    /// <inheritdoc/>
    public SdlPoint? RotationCenter {
        get => _rotationCenter;
        set => _rotationCenter = value ?? new SdlPoint(Width / 2, Height / 2);
    }
    private SdlPoint _rotationCenter;

    /// <inheritdoc/>
    public TextureFiltering TextureFiltering { get; }

    /// <inheritdoc/>
    public bool Visible { get; set; } = true;

    /// <inheritdoc/>
    public int Width { get; private set; }

    /// <inheritdoc/>
    public SdlRect? Window {
        get => _window;
        set => _window = value ?? new SdlRect(0, 0, Width, Height);
    }
    private SdlRect _window;

    /// <inheritdoc/>
    public int ZOrder { get; set; }

    /// <summary>
    /// The renderer that should be used to display this <see cref="Renderable"/>.
    /// </summary>
    private protected SdlRenderer SdlRenderer;
    
    /// <summary>
    /// The backing texture used by this <see cref="Renderable"/>.
    /// </summary>
    private protected SdlTexture SdlTexture;

    /// <summary>
    /// Creates a new <see cref="Renderable"/> with no backing texture. This should only be used in very specific
    /// circumstances, such as creating a rendering target representing the SDL window's contents.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> that should be used with this <see cref="Renderable"/>.</param>
    internal Renderable(SdlRenderer renderer) {
        SdlRenderer = renderer;
        SdlTexture = new SdlTexture();
        _blendMode = BlendMode.None;
        _nullTexture = true;

        Destination = null;
        RotationCenter = null;
        Window = null;
        
        // This stuff doesn't matter since we don't have a valid texture.
        _pixelFormat = 0;
        TextureAccess = TextureAccess.Static;
    }

    /// <summary>
    /// Creates a new <see cref="Renderable"/> based on an existing <see cref="SdlTexture"/>.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> that the <see cref="SdlTexture"/> in
    /// <paramref name="texture"/> was created with.</param>
    /// <param name="texture">The texture used to populate the new <see cref="Renderable"/>.</param>
    internal Renderable(SdlRenderer renderer, SdlTexture texture) {
        SdlRenderer = renderer;
        SdlTexture = texture;
        _ = Render.QueryTexture(texture, out var format, out var access, out var width, out var height);
        
        TextureAccess = access;
        Width = width;
        Height = height;

        Destination = null;
        RotationCenter = null;
        Window = null;

        _blendMode = BlendMode.None;
        _nullTexture = false;
        _pixelFormat = format;
    }

    /// <summary>
    /// Creates a new, blank <see cref="Renderable"/>.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> that should be used to create the backing texture.</param>
    /// <param name="access">A <see cref="Interop.Sdl.Support.Render.TextureAccess"/> value describing the type of backing texture to use for this
    /// <see cref="Image"/>.</param>
    /// <param name="width">The width of the <see cref="Renderable"/>, in pixels.</param>
    /// <param name="height">The height of this <see cref="Renderable"/>, in pixels.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="Renderable"/> should use. This
    /// defaults to <see cref="TextureFiltering.Nearest"/>.</param>
    /// <exception cref="SdlTextureInitializationException">Thrown when SDL is unable to create a texture.</exception>
    internal Renderable(SdlRenderer renderer, TextureAccess access, int width, int height, TextureFiltering textureFiltering = TextureFiltering.Nearest) {
        SdlRenderer = renderer;
        Width = width;
        Height = height;

        Destination = null;
        RotationCenter = null;
        Window = null;

        _blendMode = BlendMode.None;
        _nullTexture = false;
        TextureAccess = access;
        
        // We're only going to support 32-bit textures (for now, anyway) in the pixel format laid out by the SdlColor
        // struct. While it's unlikely that we're going to ever run on a big-endian architecture, we'll check for that
        // anyway to determine exactly which pixel format to use.
        //     Little : ABGR8888
        //     Big    : RGBA8888
        _pixelFormat = GetRgba32TextureFormat();

        // Set the scale quality (texture filtering) hint appropriately.
        TextureFiltering = textureFiltering;
        SetTextureFilteringMode(TextureFiltering);
        
        // Finally, create the texture.
        SdlTexture = Render.CreateTexture(SdlRenderer, _pixelFormat, TextureAccess, width, height);

        if(SdlTexture.IsNull) {
            var message = Error.GetError();
            throw new SdlTextureInitializationException(message);
        }
    }
    
    /// <inheritdoc/>
    public int CompareTo(IRenderable? other) {
        if(other is null) {
            return 1;
        }
        
        if(ZOrder < other.ZOrder) {
            return -1;
        }
        
        if(ZOrder > other.ZOrder) {
            return 1;
        }
            
        return 0;
    }


    /// <summary>
    /// Copies this <see cref="Renderable"/> to the active <see cref="RenderTarget"/>.
    /// </summary>
    internal virtual void Copy() {
#if DEBUG
        // Only check for rendering errors while debugging.
        var result =
#else
        _ =
#endif
        Render.RenderCopyEx(
            SdlRenderer,
            SdlTexture,
            ref _window,
            ref _destination,
            RotationAngle,
            ref _rotationCenter,
            _flipDirection
        );
        
#if DEBUG
        if(result == 0) return;
        
        var message = Error.GetError();
        throw new SdlRenderCopyException(message);
#endif
    }
    
    /// <summary>
    /// Returns the RGBA32 texture format that's applicable to this system.
    /// </summary>
    /// <returns>An unsigned integer representing the appropriate texture format.</returns>
    internal static uint GetRgba32TextureFormat() =>
        BitConverter.IsLittleEndian ? Pixels.PixelFormatAbgr8888 : Pixels.PixelFormatRgba8888;
    
    /// <summary>
    /// Releases this <see cref="Renderable"/> and all of the SDL resources attached to it. Be sure to remove this
    /// instance from any associated <see cref="RenderTarget"/> objects prior to calling this method or otherwise
    /// disposing of this object, as this will invalidate it.
    /// </summary>
    public virtual void Dispose() {
        if(Disposed) {
            return;
        }
        
        Disposed = true;
        Visible = false;
        Render.DestroyTexture(SdlTexture);
    }

    /// <inheritdoc/>
    public void Resize(int newWidth, int newHeight) {
        if(_nullTexture) {
            return;
        }
        
        Width = newWidth;
        Height = newHeight;
        
        // There's no polite way to do this--we need to destroy the old texture and recreate it.
        Render.DestroyTexture(SdlTexture);
        SdlTexture = Render.CreateTexture(SdlRenderer, _pixelFormat, TextureAccess, newWidth, newHeight);
        
        if(SdlTexture.IsNull) {
            var message = Error.GetError();
            throw new SdlTextureInitializationException(message);
        }
    }

    /// <summary>
    /// Sets the texture filtering hint. This must be used before a new SDL texture is created.
    /// </summary>
    /// <param name="textureFiltering">The <see cref="TextureFiltering"/> mode that should be set.</param>
    internal static void SetTextureFilteringMode(TextureFiltering textureFiltering) {
        _ = Hints.SetHint(
            SdlHints.RenderScaleQuality.Name,
            textureFiltering == TextureFiltering.Nearest
                ? SdlHints.RenderScaleQuality.Nearest
                : SdlHints.RenderScaleQuality.Linear
        );
    }

    /// <summary>
    /// Updates this <see cref="Renderable"/>. This is usually done prior to drawing this object to the screen.
    /// </summary>
    internal abstract void Update();
}
