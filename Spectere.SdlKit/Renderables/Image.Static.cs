using Spectere.SdlKit.Exceptions;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.Render;
using Spectere.SdlKit.Interop.SdlImage.Support.Image;
using SdlImage = Spectere.SdlKit.Interop.SdlImage.Image;

namespace Spectere.SdlKit.Renderables;

public partial class Image {
    /// <summary>
    /// If this is <c>true</c>, the SDL_image library has been initialized.
    /// </summary>
    internal static bool SdlImageInitialized { get; private set; }

    /// <summary>
    /// Loads an <see cref="Image"/> from a file on disk.
    /// </summary>
    /// <param name="renderer">The <see cref="SdlRenderer"/> that should be used to create the backing texture.</param>
    /// <param name="path">The path to the file.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="Image"/> should use. This
    /// defaults to <see cref="TextureFiltering.Nearest"/>.</param>
    /// <returns>A new <see cref="Image"/>, created from the specified file.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the file passed in via <paramref name="path"/> does not
    /// exist.</exception>
    internal static Image FromFile(SdlRenderer renderer, string path, TextureFiltering textureFiltering) {
        if(!File.Exists(path)) {
            throw new FileNotFoundException($"Asset could not be found on disk: {path}");
        }
        
        if(!SdlImageInitialized) {
            SdlImage.Init(InitFlags.InitJpg | InitFlags.InitPng);
            SdlImageInitialized = true;
        }
        
        // This is going to require a few steps to ensure that everything remains consistent:
        //
        // 1. Load the file as a surface.
        var loadedSurface = SdlImage.Load(path);
        if(loadedSurface.IsNull) {
            var sdlError = Error.GetError();
            throw new SdlImageLoadException(sdlError);
        }
        
        // 2. Convert the surface is in RGBA32 format.
        var rgba32Surface = Surface.ConvertSurfaceFormat(loadedSurface, GetRgba32TextureFormat());
        if(rgba32Surface.IsNull) {
            var sdlError = Error.GetError();
            throw new SdlFormatConversionException(sdlError);
        }

        // 3. Free the original surface.
        Surface.FreeSurface(loadedSurface);
        
        // 4. Create a texture from the surface.
        SetTextureFilteringMode(textureFiltering);
        var newTexture = Render.CreateTextureFromSurface(renderer, rgba32Surface);
        if(newTexture.IsNull) {
            var sdlError = Error.GetError();
            throw new SdlTextureInitializationException(sdlError);
        }
        
        // 5. Create an image from the texture.
        var newImage = new Image(renderer, newTexture, rgba32Surface.Pixels);
        
        // 6. Free the RGBA32 surface.
        Surface.FreeSurface(rgba32Surface);

        // fin.
        return newImage;
    }

    /// <summary>
    /// Loads an <see cref="Image"/> from a file on disk.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> whose renderer should be used to create the backing texture.</param>
    /// <param name="path">The path to the file.</param>
    /// <param name="textureFiltering">The texture filtering method that this <see cref="Image"/> should use. This
    /// defaults to <see cref="TextureFiltering.Nearest"/>.</param>
    /// <returns>A new <see cref="Image"/>, created from the specified file.</returns>
    public static Image FromFile(Window window, string path, TextureFiltering textureFiltering = TextureFiltering.Nearest)
        => FromFile(window.SdlRenderer, path, textureFiltering);
}
