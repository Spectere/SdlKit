using Spectere.SdlKit.Exceptions;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.Render;
using System.ComponentModel;

namespace Spectere.SdlKit.Renderables;

/// <summary>
/// Represents a image. This is a flexible object that can represent anything from backgrounds to sprites. 
/// </summary>
public partial class Image : Renderable {
    /// <summary>
    /// Contains the pixel data for this <see cref="Image"/>, accessible and modifiable via a variety of methods.
    /// Note that modifying one of these arrays will update all of the others, and that after modifying any of these
    /// you must call the <see cref="Update"/> method for the changes to take effect.
    /// </summary>
    public PixelData Pixels;

    /// <summary>
    /// Creates a new, blank <see cref="Image"/>.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> that the <see cref="SdlTexture"/> in
    /// <paramref name="texture"/> was created with.</param>
    /// <param name="texture">The <see cref="SdlTexture"/> that should be used as the basis for this
    /// <see cref="Image"/>.</param>
    /// <param name="pixelDataPtr">An <see cref="IntPtr"/> that points to the image data that should be used to
    /// pre-populate the <see cref="Pixels"/> array. If this is <see cref="IntPtr.Zero"/>, the Pixels array will be
    /// blank and calls to <see cref="Update"/> will clobber the existing image data.</param>
    private unsafe Image(SdlRenderer renderer, SdlTexture texture, IntPtr pixelDataPtr) : base(renderer, texture) {
        var pixelCount = Width * Height;
        
        Pixels = new PixelData(pixelCount);
        if(pixelDataPtr == IntPtr.Zero) {
            return;
        }

        var byteCount = pixelCount * 4;
        fixed(byte* destination = Pixels.ByByte) {
            var source = (byte*)pixelDataPtr.ToPointer();
            Buffer.MemoryCopy(source, destination, byteCount, byteCount);
        }
    }
    
    /// <summary>
    /// Creates a new, blank <see cref="Image"/>.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> whose renderer should be used to create the backing texture.</param>
    /// <param name="type">An <see cref="ImageType"/> describing the type of backing texture to use for this
    /// <see cref="Image"/>.</param>
    /// <param name="width">The width of the <see cref="Image"/>, in pixels.</param>
    /// <param name="height">The height of this <see cref="Image"/>, in pixels.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="Image"/> should use. This
    /// defaults to <see cref="TextureFiltering.Nearest"/>.</param>
    /// <exception cref="InvalidEnumArgumentException">Throw when an recognized value is passed to the
    /// <paramref name="type"/> argument.</exception>
    /// <exception cref="SdlTextureInitializationException">Thrown when SDL is unable to create a texture.</exception>
    public Image(Window window, ImageType type, int width, int height, TextureFiltering textureFiltering = TextureFiltering.Nearest) : base(
        window.SdlRenderer,
        type switch {
            ImageType.Static => TextureAccess.Static,
            ImageType.Streaming => TextureAccess.Streaming,
            _ => throw new InvalidEnumArgumentException($"Unrecognized ImageType: {type}")
        },
        width,
        height,
        textureFiltering
    ) {
        Pixels = new PixelData(width * height);
        Update(null);  // The SDL surface might be dirty, so do a quick update to clear it.
    }

    /// <summary>
    /// This method does nothing, as <see cref="Image"/> objects are intended to be controlled solely by the user.
    /// </summary>
    internal override void Update() { }

    /// <summary>
    /// Updates this <see cref="Image"/>. This is done by copying the data from the pixel buffer into the texture and
    /// should only be done after a change has been made.
    /// </summary>
    /// <param name="updateRegion">An <see cref="SdlRect"/> representing which section of this <see cref="Image"/> to
    /// update. If this is <c>null</c>, the entire object will be updated.</param>
    public unsafe void Update(SdlRect? updateRegion) {
        var updateRegionValue = updateRegion ?? new SdlRect(0, 0, Width, Height);
        
        fixed(byte* pixelPtr = Pixels.ByByte) {
            if(TextureAccess == TextureAccess.Streaming) {
                // Streaming texture. Lock it first so we can get a pointer to its pixels.
                _ = Render.LockTexture(SdlTexture, ref updateRegionValue, out var texPixelPtr, out var pitch);

                // Adjust the source pointer based on the update region (if applicable).
                var ptr = pixelPtr;
                if(updateRegion is not null) {
                    ptr += (updateRegion.Value.Y * pitch) + (updateRegion.Value.X * 4);
                }
                
                // Do the copy.
                if(updateRegion is null || updateRegion.Value.Width == Width) {
                    // Copy the pixels in one big block.
                    Buffer.MemoryCopy(
                        pixelPtr, texPixelPtr,
                        pitch * updateRegionValue.Height,
                        pitch * updateRegionValue.Height
                    );
                } else {
                    // Copy the pixels one row at a time.
                    for(var i = 0; i < updateRegionValue.Height; i++, ptr += pitch, texPixelPtr += pitch) {
                        Buffer.MemoryCopy(
                            ptr, texPixelPtr,
                            updateRegionValue.Width * 4,
                            updateRegionValue.Width * 4
                        );
                    }
                }
                
                // Unlock the texture.
                Render.UnlockTexture(SdlTexture);
            } else {
                // Non-streaming textures.
                _ = Render.UpdateTexture(SdlTexture, ref updateRegionValue, pixelPtr, Width * 4);
            }
        }
    }
}
