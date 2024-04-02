using Spectere.SdlKit.Exceptions;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.Render;

namespace Spectere.SdlKit.Renderables;

/// <summary>
/// Defines an SDL rendering target. This cannot be drawn to directly and instead is a collage of different
/// <see cref="Image"/> objects.
/// </summary>
public class RenderTarget : Renderable {
    private readonly SortedSet<Renderable> _renderables = new();
    
    /// <summary>
    /// If this is set to <c>true</c>, this <see cref="RenderTarget"/> will clear out its contents before updating its
    /// contents. Drawing will be slightly faster if this is set to <c>false</c>, but unless the entire texture is
    /// covered by its child <see cref="Renderable"/> objects, remnants of the previous frame's image will persist.
    /// </summary>
    public bool ClearBeforeUpdate { get; set; }

    /// <summary>
    /// Creates a new <see cref="RenderTarget"/> with a <c>null</c> backing texture. This is used to represent the
    /// SDL window.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> that should be used with this <see cref="RenderTarget"/>.</param>
    internal RenderTarget(SdlRenderer renderer) : base(renderer) {}

    /// <summary>
    /// Creates a new, blank <see cref="RenderTarget"/>.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> that should be used to create the backing texture.</param>
    /// <param name="width">The width of the <see cref="RenderTarget"/>, in pixels.</param>
    /// <param name="height">The height of this <see cref="RenderTarget"/>, in pixels.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="RenderTarget"/> should use.</param>
    /// <exception cref="SdlTextureInitializationException">Thrown when SDL is unable to create a texture.</exception>
    internal RenderTarget(SdlRenderer renderer, int width, int height, TextureFiltering textureFiltering = TextureFiltering.Nearest)
        : base(renderer, TextureAccess.Target, width, height, textureFiltering) { }

    /// <summary>
    /// Creates a new, blank <see cref="RenderTarget"/>.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> whose renderer should be used to create the backing texture.</param>
    /// <param name="width">The width of the <see cref="RenderTarget"/>, in pixels.</param>
    /// <param name="height">The height of this <see cref="RenderTarget"/>, in pixels.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="RenderTarget"/> should use.</param>
    /// <exception cref="SdlTextureInitializationException">Thrown when SDL is unable to create a texture.</exception>
    public RenderTarget(Window window, int width, int height, TextureFiltering textureFiltering = TextureFiltering.Nearest)
        : base(window.SdlRenderer, TextureAccess.Target, width, height, textureFiltering) { }

    /// <summary>
    /// Adds a renderable to this <see cref="RenderTarget"/> drawing list.
    /// </summary>
    /// <param name="renderable">The <see cref="Renderable"/> to add to the list.</param>
    public void AddRenderable(Renderable renderable) => _renderables.Add(renderable);

    /// <summary>
    /// Removes a renderable from this <see cref="RenderTarget"/> drawing list.
    /// </summary>
    /// <param name="renderable">The <see cref="Renderable"/> to remove from the list.</param>
    public void DeleteRenderable(Renderable renderable) => _renderables.Remove(renderable);

    /// <summary>
    /// Updates this <see cref="RenderTarget"/>. This is done by updating all of its child <see cref="Renderable"/>
    /// instances, then copying their contents to the target.
    /// </summary>
    internal override void Update() {
        foreach(var renderable in _renderables) {
            if(!renderable.Visible || renderable.Disposed) {
                continue;
            }

            renderable.Update();

            _ = Render.SetRenderTarget(SdlRenderer, SdlTexture);
            if(ClearBeforeUpdate) {
                _ = Render.RenderClear(SdlRenderer);
            }
            renderable.Copy();
        }
    }
}
