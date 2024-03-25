using Spectere.SdlKit.Interop.Sdl.Support.Render;
using Spectere.SdlKit.Interop.Sdl.Support.Surface;
using Spectere.SdlKit.Interop.Sdl.Support.Video;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl; 

/// <summary>
/// Contains the necessary constants and function imports from SDL_render.h.
/// </summary>
internal static class Render {
    /// <summary>
    /// Create a 2D rendering context for a window.
    /// </summary>
    /// <param name="window">The window where rendering is displayed.</param>
    /// <param name="index">The index of the rendering driver to initialize, or -1
    /// to initialize the first one supporting the requested flags.</param>
    /// <param name="flags">The requested flags for the renderer.</param>
    /// <returns>A valid rendering context or NULL if there was an error.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_CreateRenderer", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlRenderer CreateRenderer(SdlWindow window, int index, RendererFlags flags);

    /// <summary>
    /// Create a texture for a rendering context.
    /// </summary>
    /// <param name="renderer">The renderer.</param>
    /// <param name="format">The format of the texture.</param>
    /// <param name="access">The access pattern for the new texture.</param>
    /// <param name="w">The width of the texture in pixels.</param>
    /// <param name="h">The height of the texture in pixels.</param>
    /// <returns>The created texture is returned, or NULL if no rendering context was
    /// active, the format was unsupported, or the width or height were out of range.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_CreateTexture", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlTexture CreateTexture(SdlRenderer renderer, uint format, TextureAccess access, int w, int h);

    /// <summary>
    /// Create a texture from an existing surface. The surface is not modified or freed by this function. The
    /// texture access hint for the created texture is <see cref="TextureAccess.Static"/>.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="surface">The <see cref="SdlSurface"/> structure containing pixel data used to fill the
    /// texture.</param>
    /// <returns>The created texture or <c>NULL</c> on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_CreateTextureFromSurface", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlTexture CreateTextureFromSurface(SdlRenderer renderer, SdlSurface surface);

    /// <summary>
    /// Destroys a rendering context for a window and frees all associated textures.
    /// </summary>
    /// <param name="renderer">The rendering context to destroy.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_DestroyRenderer", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void DestroyRenderer(SdlRenderer renderer);

    /// <summary>
    /// Destroys a texture.
    /// </summary>
    /// <param name="texture">The texture to destroy.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_DestroyTexture", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void DestroyTexture(SdlTexture texture);

    /// <summary>
    /// Get the output size in pixels of a rendering context.
    /// </summary>
    /// <remarks>
    /// Due to high-DPI displays, you might end up with a rendering context that has more pixels than the window that
    /// contains it, so use this instead of <see cref="Video.GetWindowSize"/> to decide how much drawing area you have.
    /// </remarks>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="w">An <see cref="int"/> filled in with the width.</param>
    /// <param name="h">An <see cref="int"/> filled in with the height.</param>
    /// <returns>0 on success or a negative value on failure; call <see cref="Error.GetError"/> for more information.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetRendererOutputSize", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int GetRendererOutputSize(SdlRenderer renderer, ref int w, ref int h);

    /// <summary>
    /// Locks a portion of a texture for write-only pixel access.
    /// </summary>
    /// <param name="texture">The texture to lock for access. This texture must have been created with <see cref="TextureAccess.Streaming"/>.</param>
    /// <param name="rect">An <see cref="IntPtr"/> pointing to an <see cref="SdlRect"/>, representing an area to lock
    /// for access. If this is <see cref="IntPtr.Zero"/>, the entire texture will be locked.</param>
    /// <param name="pixels">A pointer to the locked pixels, offset by the locked area.</param>
    /// <param name="pitch">The pitch of the locked pixels. The pitch is the length of one row in bytes.</param>
    /// <returns>0 on success or a negative error code if the texture is not valid or was not created with <see cref="TextureAccess.Streaming"/>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_LockTexture", CallingConvention = CallingConvention.Cdecl)]
    internal static extern unsafe int LockTexture(SdlTexture texture, IntPtr rect, out byte* pixels, out int pitch);
    
    /// <summary>
    /// Locks a portion of a texture for write-only pixel access.
    /// </summary>
    /// <param name="texture">The texture to lock for access. This texture must have been created with <see cref="TextureAccess.Streaming"/>.</param>
    /// <param name="rect">An <see cref="SdlRect"/> structure representing the area to lock for access.</param>
    /// <param name="pixels">A pointer to the locked pixels, offset by the locked area.</param>
    /// <param name="pitch">The pitch of the locked pixels. The pitch is the length of one row in bytes.</param>
    /// <returns>0 on success or a negative error code if the texture is not valid or was not created with <see cref="TextureAccess.Streaming"/>.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_LockTexture", CallingConvention = CallingConvention.Cdecl)]
    internal static extern unsafe int LockTexture(SdlTexture texture, ref SdlRect rect, out byte* pixels, out int pitch);

    /// <summary>
    /// Query the attributes of a texture.
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <param name="format">A pointer filled in with the raw format of the texture. The actual format may differ,
    /// but pixel transfers will use this format.</param>
    /// <param name="access">A pointer filled in with the actual access to the texture (one of the
    /// <see cref="TextureAccess"/> values).</param>
    /// <param name="width">A pointer filled in with the width of the texture, in pixels.</param>
    /// <param name="height">A pointer filled in with the height of the texture, in pixels.</param>
    /// <returns>0 on success, or a negative error code on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_QueryTexture", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int QueryTexture(SdlTexture texture, out uint format, out TextureAccess access, out int width, out int height);
    
    /// <summary>
    /// Clear the current rendering target with the drawing color.
    ///
    /// This function clears the entire rendering target, ignoring the viewport and the clip rectangle.
    /// </summary>
    /// <param name="renderer">The renderer to clear.</param>
    /// <returns>0 on success, -1 on error.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RenderClear", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int RenderClear(SdlRenderer renderer);
    
    /// <summary>
    /// Copy a portion of the texture to the current rendering target.
    /// </summary>
    /// <param name="renderer">The renderer which should copy parts of a texture.</param>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcRect">The source rectangle, or <c>null</c> for the entire texture.</param>
    /// <param name="dstRect">The destination rectangle, or <c>null</c> For the entire rendering target.</param>
    /// <param name="angle">An angle, in degrees, that indicates the rotation that will be applied to <paramref name="dstRect"/>.</param>
    /// <param name="center">An <see cref="SdlPoint"/> around which <paramref name="dstRect"/> will be rotated.</param>
    /// <param name="flip">A <see cref="FlipDirection"/> value indicating which flipping actions should be performed.</param>
    /// <returns>0 on success, or -1 on error.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RenderCopyEx", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int RenderCopyEx(
        SdlRenderer renderer,
        SdlTexture texture,
        ref SdlRect srcRect,
        ref SdlRect dstRect,
        double angle,
        ref SdlPoint center,
        FlipDirection flip
    );

    /// <summary>
    /// Update the screen with the rendering performed.
    /// </summary>
    /// <param name="renderer">The renderer to update.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_RenderPresent", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void RenderPresent(SdlRenderer renderer);

    /// <summary>
    /// Sets an additional alpha value that will be multiplied into render copy operations.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="alpha">The source alpha value multiplied into copy operations.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetTextureAlphaMod", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SetTextureAlphaMod(SdlTexture texture, byte alpha);

    /// <summary>
    /// Modulates the color of a texture when it is used in a render copy operation.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="r">The red color value multiplied into copy operations.</param>
    /// <param name="g">The green color value multiplied into copy operations.</param>
    /// <param name="b">The blue color value multiplied into copy operations.</param>
    /// <returns>0 on success or a negative error code on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetTextureColorMod", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SetTextureColorMod(SdlTexture texture, byte r, byte g, byte b);

    /// <summary>
    /// Sets a texture as the current rendering target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="texture">The targeted texture, or <c>null</c> for the default render target.
    /// If a texture is used, it must have been created with the <see cref="TextureAccess.Target"/> flag.</param>
    /// <returns>Returns 0 on success or a negative error code on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetRenderTarget", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SetRenderTarget(SdlRenderer renderer, SdlTexture texture);

    /// <summary>
    /// Sets the blend mode for a texture, used by <see cref="RenderCopy"/>.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="blendMode">The <see cref="BlendMode"/> to use for texture blending.</param>
    /// <returns>Returns 0 on success or a negative error code on failure.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_SetTextureBlendMode", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SetTextureBlendMode(SdlTexture texture, BlendMode blendMode);

    /// <summary>
    /// Unlocks a texture, uploading the changes to video memory if needed.
    /// </summary>
    /// <param name="texture">A locked texture.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_UnlockTexture", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void UnlockTexture(SdlTexture texture);

    /// <summary>
    /// Update the given texture rectangle with new pixel data.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="rect">A rectangle of pixels to update, or <see cref="IntPtr.Zero"/> to update the entire
    /// texture.</param>
    /// <param name="pixels">The raw pixel data.</param>
    /// <param name="pitch">The number of bytes in a row of pixel data, including padding between lines.</param>
    /// <returns>0 on success, or -1 if the texture is not valid.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_UpdateTexture", CallingConvention = CallingConvention.Cdecl)]
    internal static extern unsafe int UpdateTexture(SdlTexture texture, IntPtr rect, void* pixels, int pitch);
    
    /// <summary>
    /// Update the given texture rectangle with new pixel data.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="rect">A rectangle of pixels to update.</param>
    /// <param name="pixels">The raw pixel data.</param>
    /// <param name="pitch">The number of bytes in a row of pixel data, including padding between lines.</param>
    /// <returns>0 on success, or -1 if the texture is not valid.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_UpdateTexture", CallingConvention = CallingConvention.Cdecl)]
    internal static extern unsafe int UpdateTexture(SdlTexture texture, ref SdlRect rect, void* pixels, int pitch);
}
