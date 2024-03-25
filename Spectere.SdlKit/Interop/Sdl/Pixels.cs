using Spectere.SdlKit.Interop.Sdl.Support.Pixels;

namespace Spectere.SdlKit.Interop.Sdl {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_pixels.h.
    /// </summary>
    internal static class Pixels {
        /// <summary>
        /// Defines a pixel format.
        /// </summary>
        /// <param name="type">Describes the way pixels are packed.</param>
        /// <param name="order">The order that the pixel components are stored in.</param>
        /// <param name="layout">The layout of the pixels.</param>
        /// <param name="bits">The number of bits per pixel.</param>
        /// <param name="bytes">The number of bytes per pixel.</param>
        /// <returns>A packed 32-bit integer representing the resulting pixel format.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        internal static uint DefinePixelFormat(PixelTypes type, ComponentOrder order, PackedComponentLayout layout, byte bits, byte bytes) {
            return (uint)(
                  (1 << 28)
                | ((int)type << 24)
                | ((int)order << 20)
                | ((int)layout << 16)
                | (bits << 8)
                | bytes
            );
        }

        /*
         * Pixel formats cannot be easily expressed in an enum, so we'll make them constants instead.
         */
        
        /// <summary>Indexed, 1bpp, least significant bit first.</summary>
        internal static readonly uint PixelFormatIndex1Lsb = DefinePixelFormat(PixelTypes.Index1, ComponentOrder.Bitmap4321, PackedComponentLayout.LayoutNone, 1, 0);
        
        /// <summary>Indexed, 1bpp, most significant bit first.</summary>
        internal static readonly uint PixelFormatIndex1Msb = DefinePixelFormat(PixelTypes.Index1, ComponentOrder.Bitmap1234, PackedComponentLayout.LayoutNone, 1, 0);
        
        /// <summary>Indexed, 4bpp, least significant bits first.</summary>
        internal static readonly uint PixelFormatIndex4Lsb = DefinePixelFormat(PixelTypes.Index4, ComponentOrder.Bitmap4321, PackedComponentLayout.LayoutNone, 4, 0);
        
        /// <summary>Indexed, 4bpp, most significant bits first.</summary>
        internal static readonly uint PixelFormatIndex4Msb = DefinePixelFormat(PixelTypes.Index4, ComponentOrder.Bitmap1234, PackedComponentLayout.LayoutNone, 4, 0);
        
        /// <summary>Indexed, 8bpp.</summary>
        internal static readonly uint PixelFormatIndex8 = DefinePixelFormat(PixelTypes.Index8, ComponentOrder.None, PackedComponentLayout.LayoutNone, 8, 1);
        
        /// <summary>Packed 8bpp, R3/G3/B2.</summary>
        internal static readonly uint PixelFormatRgb332 = DefinePixelFormat(PixelTypes.Packed8, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout332, 8, 1);
        
        /// <summary>Packed 16-bit, 12bpp, X4/R4/G4/B4.</summary>
        internal static readonly uint PixelFormatRgb444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout4444, 12, 2);
        
        /// <summary>Packed 16-bit, 15bpp, X1/R5/G5/B5.</summary>
        internal static readonly uint PixelFormatRgb555 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout1555, 15, 2);
        
        /// <summary>Packed 16-bit, 15bpp, X1/B5/G5/R5.</summary>
        internal static readonly uint PixelFormatBgr555 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXbgr, PackedComponentLayout.Layout1555, 15, 2);
        
        /// <summary>Packed 16-bit, 16bpp, A4/R4/G4/B4.</summary>
        internal static readonly uint PixelFormatArgb4444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedArgb, PackedComponentLayout.Layout4444, 16, 2);
        
        /// <summary>Packed 16-bit, 16bpp, R4/G4/B4/A4.</summary>
        internal static readonly uint PixelFormatRgba4444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedRgba, PackedComponentLayout.Layout4444, 16, 2);
        
        /// <summary>Packed 16-bit, 16bpp, A4/B4/G4/R4.</summary>
        internal static readonly uint PixelFormatAbgr4444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedAbgr, PackedComponentLayout.Layout4444, 16, 2);
        
        /// <summary>Packed 16-bit, 16bpp, B4/G4/R4/A4.</summary>
        internal static readonly uint PixelFormatBgra4444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedBgra, PackedComponentLayout.Layout4444, 16, 2);
        
        /// <summary>Packed 16-bit, 16bpp, A1/R4/G4/B4.</summary>
        internal static readonly uint PixelFormatArgb1555 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedArgb, PackedComponentLayout.Layout1555, 16, 2);
        
        /// <summary>Packed 16-bit, 16bpp, R5/G5/B5/A1.</summary>
        internal static readonly uint PixelFormatRgba5551 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedRgba, PackedComponentLayout.Layout5551, 16, 2);
        
        /// <summary>Packed 16-bit, 16bpp, A1/B5/G5/R5.</summary>
        internal static readonly uint PixelFormatAbgr1555 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedAbgr, PackedComponentLayout.Layout1555, 16, 2);
        
        /// <summary>Packed 16-bit, 16bpp, B5/G5/R5/A1.</summary>
        internal static readonly uint PixelFormatBgra5551 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedBgra, PackedComponentLayout.Layout5551, 16, 2);
        
        /// <summary>Packed 16-bit, 16bpp, R5/G6/B5.</summary>
        internal static readonly uint PixelFormatRgb565 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout565, 16, 2);
        
        /// <summary>Packed 16-bit, 16bpp, B5/G6/R5.</summary>
        internal static readonly uint PixelFormatBgr565 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXbgr, PackedComponentLayout.Layout565, 16, 2);
        
        /// <summary>8-bit unsigned integer array, 24bpp, RGB order.</summary>
        internal static readonly uint PixelFormatRgb24 = DefinePixelFormat(PixelTypes.ArrayU8, ComponentOrder.ArrayRgb, PackedComponentLayout.LayoutNone, 24, 3);
        
        /// <summary>8-bit unsigned integer array, 24bpp, BGR order.</summary>
        internal static readonly uint PixelFormatBgr24 = DefinePixelFormat(PixelTypes.ArrayU8, ComponentOrder.ArrayBgr, PackedComponentLayout.LayoutNone, 24, 3);
        
        /// <summary>Packed 32-bit, 24bpp, X8/R8/G8/B8.</summary>
        internal static readonly uint PixelFormatRgb888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout8888, 24, 4);
        
        /// <summary>Packed 32-bit, 24bpp, R8/G8/B8/X8.</summary>
        internal static readonly uint PixelFormatRgbx8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedRgbx, PackedComponentLayout.Layout8888, 24, 4);
        
        /// <summary>Packed 32-bit, 24bpp, X8/B8/G8/R8.</summary>
        internal static readonly uint PixelFormatBgr888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedXbgr, PackedComponentLayout.Layout8888, 24, 4);
        
        /// <summary>Packed 32-bit, 24bpp, B8/G8/R8/X8.</summary>
        internal static readonly uint PixelFormatBgrx8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedBgrx, PackedComponentLayout.Layout8888, 24, 4);
        
        /// <summary>Packed 32-bit, 32bpp, A8/R8/G8/B8.</summary>
        internal static readonly uint PixelFormatArgb8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedArgb, PackedComponentLayout.Layout8888, 32, 4);
        
        /// <summary>Packed 32-bit, 32bpp, R8/G8/B8/A8.</summary>
        internal static readonly uint PixelFormatRgba8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedRgba, PackedComponentLayout.Layout8888, 32, 4);
        
        /// <summary>Packed 32-bit, 32bpp, A8/B8/G8/R8.</summary>
        internal static readonly uint PixelFormatAbgr8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedAbgr, PackedComponentLayout.Layout8888, 32, 4);
        
        /// <summary>Packed 32-bit, 32bpp, B8/G8/R8/A8.</summary>
        internal static readonly uint PixelFormatBgra8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedBgra, PackedComponentLayout.Layout8888, 32, 4);
        
        /// <summary>Packed 32-bit, 32bpp, A2/R10/G10/B10.</summary>
        internal static readonly uint PixelFormatArgb2101010 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.ArrayArgb, PackedComponentLayout.Layout2101010, 32, 4);
    }
}
