using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Pixels;

/// <summary>
/// Defines an SDL palette.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct Palette {
    internal int Count;
    internal unsafe SdlColor* Colors;
    internal uint Version;
    internal int RefCount;
}
