using Spectere.SdlKit.Interop.Sdl.Support.Audio;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl; 

/// <summary>
/// Contains the necessary constants and function imports from SDL_audio.h.
/// </summary>
internal static class Audio {
    private const ushort FormatMaskBitDepth = 0xFF;
    private const ushort FormatMaskDataType = 1 << 8;
    private const ushort FormatMaskEndian = 1 << 12;
    private const ushort FormatMaskSigned = 1 << 15;

    /// <summary>
    /// Returns the bit depth of the audio format.
    /// </summary>
    /// <param name="format">The SDL audio format.</param>
    /// <returns>The bit depth of the audio format.</returns>
    internal static byte BitDepth(ushort format) {
        return (byte)(format & FormatMaskBitDepth);
    }

    /// <summary>
    /// Determines whether the specified format is big-endian.
    /// </summary>
    /// <param name="format">The SDL audio format.</param>
    /// <returns><c>true</c> if the audio format is big-endian, otherwise <c>false</c>.</returns>
    internal static bool IsBigEndian(ushort format) {
        return (format & FormatMaskEndian) != 0;
    }

    /// <summary>
    /// Determines whether the specified format is a floating-point format.
    /// </summary>
    /// <param name="format">The SDL audio format.</param>
    /// <returns><c>true</c> if the audio format is represented by floats, otherwise <c>false</c>.</returns>
    internal static bool IsFloat(ushort format) {
        return (format & FormatMaskDataType) != 0;
    }

    /// <summary>
    /// Determines whether the specified format is a signed integral format.
    /// </summary>
    /// <param name="format">The SDL audio format.</param>
    /// <returns><c>true</c> if the audio format is a signed integral format, otherwise <c>false</c>.</returns>
    internal static bool IsSigned(ushort format) {
        return (format & FormatMaskSigned) != 0;
    }

    /// <summary>
    /// A function pointer representing the audio callback function.
    /// </summary>
    /// <param name="userdata">Userdata passed to the function.</param>
    /// <param name="stream">The stream to be filled in by the application.</param>
    /// <param name="len">The length of the array in <paramref name="stream"/>.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SdlAudioCallback(IntPtr userdata, IntPtr stream, int len);

    /// <summary>
    /// Shuts down audio processing and closes the audio device.
    /// </summary>
    /// <param name="dev">An audio device previously opened by <see cref="OpenAudioDevice(IntPtr,int,ref AudioSpec,out AudioSpec,AllowedChanges)"/>.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_CloseAudioDevice", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void CloseAudioDevice(uint dev);

    /// <summary>
    /// Open a specific audio device. Passing in a device name of NULL requests the most reasonable default (and is
    /// equivalent to calling SDL_OpenAudio).
    /// </summary>
    /// <param name="device">The name of the device to open, as reported SDL_GetAudioDriverName(). Some drivers allow
    /// arbitrary and driver-specific strings, such as a hostname/IP address for a remote audio server, or a filename
    /// in the disk audio driver.</param>
    /// <param name="isCapture">Non-zero to specify a device should be opened for recording, not playback.</param>
    /// <param name="desired">An <see cref="AudioSpec"/> structure representing the desired output format.</param>
    /// <param name="obtained">An <see cref="AudioSpec"/> structure filled in with the actual output format.</param>
    /// <param name="allowedChanges">If this is <c>true</c>, the <paramref name="obtained"/> <see cref="AudioSpec"/> is
    /// allowed to differ from the <paramref name="desired"/> specs.</param>
    /// <returns>A non-zero value on success or zero on failure. This will never return 1, since SDL reserves that ID
    /// for the SDL_OpenAudio() function.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_OpenAudioDevice", CallingConvention = CallingConvention.Cdecl)]
    internal static extern uint OpenAudioDevice(IntPtr device, int isCapture, ref AudioSpec desired, out AudioSpec obtained, AllowedChanges allowedChanges);

    /// <summary>
    /// Open a specific audio device. Passing in a device name of NULL requests the most reasonable default (and is
    /// equivalent to calling SDL_OpenAudio).
    /// </summary>
    /// <param name="device">The name of the device to open, as reported SDL_GetAudioDriverName(). Some drivers allow
    /// arbitrary and driver-specific strings, such as a hostname/IP address for a remote audio server, or a filename
    /// in the disk audio driver.</param>
    /// <param name="isCapture">Non-zero to specify a device should be opened for recording, not playback.</param>
    /// <param name="desired">An <see cref="AudioSpec"/> structure representing the desired output format.</param>
    /// <param name="obtained">An <see cref="AudioSpec"/> structure filled in with the actual output format.</param>
    /// <param name="allowedChanges">If this is <c>true</c>, the <paramref name="obtained"/> <see cref="AudioSpec"/> is
    /// allowed to differ from the <paramref name="desired"/> specs.</param>
    /// <returns>A non-zero value on success or zero on failure. This will never return 1, since SDL reserves that ID
    /// for the SDL_OpenAudio() function.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_OpenAudioDevice", CallingConvention = CallingConvention.Cdecl)]
    internal static extern uint OpenAudioDevice(string device, int isCapture, ref AudioSpec desired, out AudioSpec obtained, AllowedChanges allowedChanges);

    /// <summary>
    /// Pause and unpause audio callback processing.
    /// </summary>
    /// <param name="dev">The audio device to change.</param>
    /// <param name="pauseOn">The <see cref="AudioState"/> to switch to.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_PauseAudioDevice", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void PauseAudioDevice(uint dev, AudioState pauseOn);
}
