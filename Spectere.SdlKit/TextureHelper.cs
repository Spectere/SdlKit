using SdlImage = Spectere.SdlKit.Interop.SdlImage.Image;
using Spectere.SdlKit.Exceptions;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.Render;

namespace Spectere.SdlKit;

/// <summary>
/// Methods that help with creating and managing textures.
/// </summary>
internal static class TextureHelper {
    /// <summary>
    /// Loads an <see cref="SdlTexture"/> from a given file.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> used to create the texture.</param>
    /// <param name="texture">A reference to the <see cref="SdlTexture"/> object. This will be updated by this
    /// method.</param>
    /// <param name="filename">The path pointing to the file that should be loaded.</param>
    /// <param name="textureFiltering">The <see cref="TextureFiltering"/> hint that should be applied to this
    /// texture.</param>
    /// <exception cref="FileNotFoundException">Thrown if the file specified by <paramref name="filename"/> could not
    /// be found on disk.</exception>
    /// <exception cref="SdlImageLoadException">Thrown if the image could not be loaded by SDL_image.</exception>
    /// <exception cref="SdlFormatConversionException">Thrown if the file could not be converted to the appropriate
    /// texture format.</exception>
    /// <exception cref="SdlTextureInitializationException">Thrown if the texture could not be initialized.</exception>
    internal static TextureInformation LoadTextureFromFile(ref SdlRenderer renderer, ref SdlTexture texture, string filename, TextureFiltering textureFiltering) {
        if(!File.Exists(filename)) {
            throw new FileNotFoundException($"Image could not be found on disk: {filename}");
        }
        
        if(!texture.IsNull) {
            // Destroy the existing texture.
            Render.DestroyTexture(texture);
        }
        
        // Set the scale quality hint appropriately.
        SdlHintHelper.SetTextureFilteringMode(textureFiltering);
        
        // Load the font as a surface.
        var loadedSurface = SdlImage.Load(filename);
        if(loadedSurface.IsNull) {
            var sdlError = Error.GetError();
            throw new SdlImageLoadException(sdlError);
        }
        
        // Convert the surface to RGBA32.
        var rgba32Surface = Surface.ConvertSurfaceFormat(loadedSurface, Renderable.GetRgba32TextureFormat());
        if(rgba32Surface.IsNull) {
            var sdlError = Error.GetError();
            throw new SdlFormatConversionException(sdlError);
        }

        // Free the original surface.
        Surface.FreeSurface(loadedSurface);

        // Create the hardware accelerated texture.
        SdlHintHelper.SetTextureFilteringMode(textureFiltering);
        texture = Render.CreateTextureFromSurface(renderer, rgba32Surface);
        if(texture.IsNull) {
            var sdlError = Error.GetError();
            throw new SdlTextureInitializationException(sdlError);
        }

        // Free the RGBA32 surface.
        Surface.FreeSurface(rgba32Surface);
        
        // Query the texture to get information for the calling method.
        _ = Render.QueryTexture(texture, out _, out _, out var textureWidth, out var textureHeight);
        return new TextureInformation(textureWidth, textureHeight);
    }
}
