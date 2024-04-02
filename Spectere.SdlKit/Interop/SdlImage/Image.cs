using Spectere.SdlKit.Interop.Sdl.Support.Render;
using Spectere.SdlKit.Interop.Sdl.Support.RwOps;
using Spectere.SdlKit.Interop.Sdl.Support.Surface;
using Spectere.SdlKit.Interop.SdlImage.Support.Image;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.SdlImage;

/// <summary>
/// Contains the necessary constants and function imports from SDL_image.h.
/// </summary>
public class Image {
    /// <summary>
    /// Dispose of an <see cref="Animation"/> and frees its resources.
    /// </summary>
    /// <param name="anim">The <see cref="Animation"/> to dispose of.</param>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_FreeAnimation", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void FreeAnimation(Animation anim);
    
    /// <summary>
    /// Initialize SDL_image. This function loads dynamic libraries that SDL_image needs, and prepares them for use.
    /// This must be the first function you call in SDL_image, and if it fails you should not continue with the library.
    /// </summary>
    /// <param name="flags">Initialization flags, OR'd together.</param>
    /// <returns>All currently initialized flags.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_Init", CallingConvention = CallingConvention.Cdecl)]
    internal static extern InitFlags Init(InitFlags flags);
    
    /// <summary>
    /// Detect AVIF image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is AVIF data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isAVIF", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsAvif(SdlRwOps src);

    /// <summary>
    /// Detect BMP image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is BMP data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isBMP", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsBmp(SdlRwOps src);
    
    /// <summary>
    /// Detect CUR image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is CUR data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isCUR", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsCur(SdlRwOps src);

    /// <summary>
    /// Detect GIF image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is GIF data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isGIF", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsGif(SdlRwOps src);

    /// <summary>
    /// Detect ICO image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is ICO data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isICO", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsIco(SdlRwOps src);

    /// <summary>
    /// Detect JPEG image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is JPEG data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isJPG", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsJpg(SdlRwOps src);

    /// <summary>
    /// Detect JPEG XL image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is JPEG XL data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isJXL", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsJxl(SdlRwOps src);

    /// <summary>
    /// Detect LBM image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is LBM data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isLBM", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsLbm(SdlRwOps src);

    /// <summary>
    /// Detect PCX image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is PCX data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isPCX", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsPcx(SdlRwOps src);

    /// <summary>
    /// Detect PNG image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is PNG data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isPNG", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsPng(SdlRwOps src);

    /// <summary>
    /// Detect PNM image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is PNM data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isPNM", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsPnm(SdlRwOps src);

    /// <summary>
    /// Detect QOI image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is QOI data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isQOI", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsQoi(SdlRwOps src);

    /// <summary>
    /// Detect SVG image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is SVG data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isSVG", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsSvg(SdlRwOps src);

    /// <summary>
    /// Detect TIFF image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is TIFF data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isTIF", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsTif(SdlRwOps src);

    /// <summary>
    /// Detect WebP image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is WebP data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isWEBP", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsWebP(SdlRwOps src);

    /// <summary>
    /// Detect XCF image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is XCF data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isXCF", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsXcf(SdlRwOps src);

    /// <summary>
    /// Detect XPM image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is XPM data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isXPM", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsXpm(SdlRwOps src);

    /// <summary>
    /// Detect XV image data on a readable/seekable SDL RWops.
    /// </summary>
    /// <remarks>
    /// There is no distinction made between "not the file type in question" and basic I/O errors.
    /// </remarks>
    /// <param name="src">A seekable/readable SDL RWops to provide image data.</param>
    /// <returns>Non-zero if this is XV data, otherwise zero.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_isXV", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int IsXv(SdlRwOps src);
    
    /// <summary>
    /// Load an image from a filesystem path into a software surface.
    /// </summary>
    /// <remarks>
    /// There are no guarantees about what format the new surface will be. In many cases, SDL_image will attempt
    /// to supply a surface that exactly matches the provided image, but in others it might have to convert (either
    /// because the image is in a format that SDL doesn't directly support or because it's compressed data that could
    /// reasonably decompress to various formats and SDL_image has to pick one).
    /// </remarks>
    /// <param name="file">A path on the filesystem to load an image from.</param>
    /// <returns>A new <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_Load", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface Load(string file);

    /// <summary>
    /// Loads an animation from a file. When done with the returned animation, the app should dispose of it with a call
    /// to <see cref="FreeAnimation"/>.
    /// </summary>
    /// <param name="file">The path on the filesystem containing an animated image.</param>
    /// <returns>A new <see cref="Animation"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadAnimation", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ref Animation? LoadAnimation(string file);

    /// <summary>
    /// Loads an animation from an SDL data source. When done with the returned animation, the app should dispose of it
    /// with a call to <see cref="FreeAnimation"/>.
    /// </summary>
    /// <param name="src">An SDL RWops that data will be read from.</param>
    /// <param name="freeSrc">Non-zero to close/free to SDL RWops before returning, or zero to leave it open.</param>
    /// <returns>A new <see cref="Animation"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadAnimation_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ref Animation? LoadAnimationRw(SdlRwOps src, int freeSrc);

    /// <summary>
    /// Loads an animation from an SDL data source. When done with the returned animation, the app should dispose of it
    /// with a call to <see cref="FreeAnimation"/>.
    /// </summary>
    /// <param name="src">An SDL RWops that data will be read from.</param>
    /// <param name="freeSrc">Non-zero to close/free to SDL RWops before returning, or zero to leave it open.</param>
    /// <param name="type">A filename extension that represents this data ("GIF", etc.).</param>
    /// <returns>A new <see cref="Animation"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadAnimationTyped_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ref Animation? LoadAnimationTypedRw(SdlRwOps src, int freeSrc, string type);

    /// <summary>
    /// Load an image in the AVIF format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadAVIF_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadAvifRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the BMP format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadBMP_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadBmpRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the CUR format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadCUR_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadCurRw(SdlRwOps src);

    /// <summary>
    /// Load a GIF animation directly. If you know the format of the animation, you can call this function, which will
    /// skip SDL_image's file format detection routines. Generally it's better to use the abstract interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops that data will be read from.</param>
    /// <returns>A new <see cref="Animation"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadGIFAnimation_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ref Animation? LoadGifAnimationRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the GIF format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadGIF_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadGifRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the JPEG format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadJPG_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadJpgRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the JPEG XL format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadJXL_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadJxlRw(SdlRwOps src);
    
    /// <summary>
    /// Load an image in the ICO format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadICO_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadIcoRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the LBM format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadLBM_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadLbmRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the PCX format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadPCX_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadPcxRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the PNG format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadPNG_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadPngRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the PNM format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadPNM_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadPnmRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the QOI format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadQOI_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadQoiRw(SdlRwOps src);

    /// <summary>
    /// Load an image from an SDL data source into a software surface.
    /// </summary>
    /// <remarks>
    /// There are no guarantees about what format the new surface will be. In many cases, SDL_image will attempt
    /// to supply a surface that exactly matches the provided image, but in others it might have to convert (either
    /// because the image is in a format that SDL doesn't directly support or because it's compressed data that could
    /// reasonably decompress to various formats and SDL_image has to pick one).
    /// </remarks>
    /// <param name="src">An SDL RWops that data will be read from.</param>
    /// <param name="freeSrc">Non-zero to close/free the SDL RWops before returning, zero to leave it open.</param>
    /// <returns>A new <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_Load_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadRw(SdlRwOps src, int freeSrc);
    
    /// <summary>
    /// Loads an SVG image, scaled to a specific size. Since SVG files are resolution-independent, you specify the size
    /// you would like the output image to be and it will be generated at those resolutions. Either <paramref name="width"/>
    /// or <paramref name="height"/> may be 0 and the image will be auto-sized to preserve aspect ratio.
    /// </summary>
    /// <param name="src">An SDL RWops to load SVG data from.</param>
    /// <param name="width">The desired width of the generated surface, in pixels.</param>
    /// <param name="height">The desired height of the generated surface, in pixels.</param>
    /// <returns>A new <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadSizedSVG_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadSizedSvgRw(SdlRwOps src, int width, int height);

    /// <summary>
    /// Load an image in the SVG format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadSVG_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadSvgRw(SdlRwOps src);

    /// <summary>
    /// Load an image from a filesystem into a GPU texture.
    /// </summary>
    /// <remarks>
    /// If the loaded image has transparency or a color key, a texture with an alpha channel will be created. Otherwise,
    /// SDL_image will attempt to create a texture in a format that most reasonably represents the image data (but in
    /// many cases, this will just end up being 32-bit RGB or 32-bit RGBA).
    /// </remarks>
    /// <param name="renderer">The renderer to use to create the GPU texture.</param>
    /// <param name="file">A path on the filesystem to load an image from.</param>
    /// <returns>A new <see cref="SdlTexture"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadTexture", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ref SdlTexture LoadTexture(SdlRenderer renderer, string file);

    /// <summary>
    /// Loads an image from an SDL data source into a GPU texture.
    /// </summary>
    /// <remarks>
    /// If the loaded image has transparency or a color key, a texture with an alpha channel will be created. Otherwise,
    /// SDL_image will attempt to create a texture in a format that most reasonably represents the image data (but in
    /// many cases, this will just end up being 32-bit RGB or 32-bit RGBA).
    /// </remarks>
    /// <param name="renderer">The renderer to use to create the GPU texture.</param>
    /// <param name="src">An SDL RWops that data will be read from.</param>
    /// <param name="freeSrc">Non-zero to close/free the SDL RWops before returning, zero to leave it open.</param>
    /// <returns>A new <see cref="SdlTexture"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadTexture_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ref SdlTexture LoadTextureRw(SdlRenderer renderer, SdlRwOps src, int freeSrc);

    /// <summary>
    /// Loads an image from an SDL data source into a GPU texture.
    /// </summary>
    /// <remarks>
    /// If the loaded image has transparency or a color key, a texture with an alpha channel will be created. Otherwise,
    /// SDL_image will attempt to create a texture in a format that most reasonably represents the image data (but in
    /// many cases, this will just end up being 32-bit RGB or 32-bit RGBA).
    /// </remarks>
    /// <param name="renderer">The renderer to use to create the GPU texture.</param>
    /// <param name="src">An SDL RWops that data will be read from.</param>
    /// <param name="freeSrc">Non-zero to close/free the SDL RWops before returning, zero to leave it open.</param>
    /// <param name="type">A filename extension that represents this data ("BMP", "GIF", "PNG", etc.).</param>
    /// <returns>A new <see cref="SdlTexture"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadTextureTyped_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern ref SdlTexture LoadTextureTypedRw(SdlRenderer renderer, IntPtr src, int freeSrc, string type);
    
        /// <summary>
    /// Load an image in the TGA format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadTGA_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadTgaRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the TIF format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadTIF_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadTifRw(SdlRwOps src);
    
    /// <summary>
    /// Load an image from an SDL data source into a software surface.
    /// </summary>
    /// <remarks>
    /// There are no guarantees about what format the new surface will be. In many cases, SDL_image will attempt
    /// to supply a surface that exactly matches the provided image, but in others it might have to convert (either
    /// because the image is in a format that SDL doesn't directly support or because it's compressed data that could
    /// reasonably decompress to various formats and SDL_image has to pick one).
    /// </remarks>
    /// <param name="src">An SDL RWops that data will be read from.</param>
    /// <param name="freeSrc">Non-zero to close/free the SDL RWops before returning, zero to leave it open.</param>
    /// <param name="type">A filename extension that represents this data ("BMP", "GIF", "PNG", etc.).</param>
    /// <returns>A new <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadTyped_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadTypedRw(SdlRwOps src, int freeSrc, string type);
    
    /// <summary>
    /// Load a WebP animation directly. If you know the format of the animation, you can call this function, which will
    /// skip SDL_image's file format detection routines. Generally it's better to use the abstract interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops that data will be read from.</param>
    /// <returns>A new <see cref="Animation"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadWEBPAnimation_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern Animation? LoadWebPAnimationRw(SdlRwOps src);
    
    /// <summary>
    /// Load an image in the WebP format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadWEBP_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadWebPRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the XCF format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadXCF_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadXcfRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the XPM format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadXPM_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadXpmRw(SdlRwOps src);

    /// <summary>
    /// Load an image in the XV format directly. If you know the format of the image, you can call this function,
    /// which will skip SDL_image's file format detection routines. Generally it's better to use the abstract
    /// interfaces.
    /// </summary>
    /// <param name="src">An SDL RWops to load image data from.</param>
    /// <returns>An <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_LoadXV_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface LoadXvRw(SdlRwOps src);

    /// <summary>
    /// Deinitialize SDL_image. This should be the last function you call in SDL_image, after freeing all other
    /// resources. This will unload any shared libraries it is using for various codecs. After this call, a call to
    /// <see cref="Init"/> will return <see cref="InitFlags.None"/>.
    /// </summary>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_Quit", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void Quit();

    /// <summary>
    /// Load an XPM image from a memory array. The returned surface will be an 8bpp indexed surface, if possible,
    /// otherwise it will be 32bpp. If you always want 32-bit data, use <see cref="ReadXPMFromArrayToRGB888"/> instead.
    /// </summary>
    /// <param name="xpm">A null-terminated array of strings that comprise XPM data.</param>
    /// <returns>A new <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_ReadXPMFromArray", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface ReadXpmFromArray([In] string[] xpm);

    /// <summary>
    /// Load an XPM image from a memory array. The returned surface will always be a 32-bit RGB surface. If you want
    /// 8-bit indexed colors (and the XPM data allows it) use <see cref="ReadXpmFromArray"/> instead.
    /// </summary>
    /// <param name="xpm">A null-terminated array of strings that comprise XPM data.</param>
    /// <returns>A new <see cref="SdlSurface"/>, or <c>null</c> on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_ReadXPMFromArrayToRGB888", CallingConvention = CallingConvention.Cdecl)]
    internal static extern SdlSurface ReadXPMFromArrayToRGB888([In] string[] xpm);

    /// <summary>
    /// Save an <see cref="SdlSurface"/> into a JPEG image file. If the file already exists, it will be overwritten.
    /// </summary>
    /// <param name="surface">The <see cref="SdlSurface"/> to save.</param>
    /// <param name="file">The path on the filesystem to write the new file to.</param>
    /// <param name="quality">A quality value from 0 to 100, inclusive. The higher the number, the higher the
    /// quality.</param>
    /// <returns>0 if successful, or -1 on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_SaveJPG", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SaveJpg(SdlSurface surface, string file, int quality);

    /// <summary>
    /// Save an <see cref="SdlSurface"/> into JPEG image data, via an SDL RWops.
    /// </summary>
    /// <param name="surface">The <see cref="SdlSurface"/> to save.</param>
    /// <param name="dst">The SDL RWops to save the image data to.</param>
    /// <param name="freeDst">Non-zero to close/free the SDL RWops before returning, zero to leave it open.</param>
    /// <param name="quality">A quality value from 0 to 100, inclusive. The higher the number, the higher the
    /// quality.</param>
    /// <returns>0 if successful, or -1 on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_SaveJPG_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SaveJpgRw(SdlSurface surface, IntPtr dst, int freeDst, int quality);

    /// <summary>
    /// Save an <see cref="SdlSurface"/> into a PNG image file. If the file already exists, it will be overwritten.
    /// </summary>
    /// <param name="surface">The <see cref="SdlSurface"/> to save.</param>
    /// <param name="file">The path on the filesystem to write the new file to.</param>
    /// <returns>0 if successful, or -1 on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_SavePNG", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SavePng(SdlSurface surface, string file);

    /// <summary>
    /// Save an <see cref="SdlSurface"/> into PNG image data, via an SDL RWops.
    /// </summary>
    /// <param name="surface">The <see cref="SdlSurface"/> to save.</param>
    /// <param name="dst">The SDL RWops to save the image data to.</param>
    /// <param name="freeDst">Non-zero to close/free the SDL RWops before returning, zero to leave it open.</param>
    /// <returns>0 if successful, or -1 on error.</returns>
    [DllImport(Lib.Sdl2Image, EntryPoint = "IMG_SavePNG_RW", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SavePngRw(SdlSurface surface, SdlRwOps dst, int freeDst);
}
