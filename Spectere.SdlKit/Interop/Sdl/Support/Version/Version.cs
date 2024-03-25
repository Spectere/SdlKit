using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl.Support.Version;

/// <summary>
/// Information about the version of SDL in use.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Version {
    /// <summary>The major component of the version number.</summary>
    public byte Major;
    
    /// <summary>The minor component of the version number.</summary>
    public byte Minor;
    
    /// <summary>The update component of the version number.</summary>
    public byte Patch;

    /// <summary>
    /// Formats the version number as a string in X.Y.Z format, where X is the <see cref="Major"/> version component,
    /// Y is the <see cref="Minor"/> component, and Z is the <see cref="Patch"/> component.
    /// </summary>
    /// <returns>A formatted version string.</returns>
    public override string ToString() => $"{Major}.{Minor}.{Patch}";
}
