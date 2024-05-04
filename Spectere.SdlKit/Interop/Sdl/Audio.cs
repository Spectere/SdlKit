using Spectere.SdlKit.Interop.Sdl.Support.Audio;
using System.Runtime.InteropServices;

namespace Spectere.SdlKit.Interop.Sdl; 

/// <summary>
/// Contains the necessary constants and function imports from SDL_audio.h.
/// </summary>
internal static class Audio {
    /// <summary>
    /// The mask used to get the big endian flag from the SDL audio format values.
    /// </summary>
    internal const int BigEndianMask = 1 << 8;
    
    /// <summary>
    /// The mask used to get the bit size from the SDL audio format values. 
    /// </summary>
    internal const int BitSizeMask = 0xFF;
    
    /// <summary>
    /// The mask used to get the flating point flag from the SDL audio format values.
    /// </summary>
    internal const int FloatMask = 1 << 12;
    
    /// <summary>
    /// The mask used to get the signed flag from the SDL audio format values.
    /// </summary>
    internal const int SignedMask = 1 << 15;
    
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
    /// <param name="dev">An audio device previously opened by <see cref="OpenAudioDevice(IntPtr,int,ref SdlAudioSpec,out SdlAudioSpec,AllowedAudioSpecChanges)"/>.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_CloseAudioDevice", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void CloseAudioDevice(uint dev);
    
    /// <summary>
    /// Get the human-readable name of a specific audio device. The values returned by this function reflect the latest
    /// call to <see cref="GetNumAudioDevices"/>. That function must be called again to redetect available hardware.
    /// </summary>
    /// <param name="index">The index of the audio device. Valid values range from 0 to <see cref="GetNumAudioDevices"/>
    /// - 1.</param>
    /// <param name="isCapture">Non-zero to query the list of recording devices or zero to query the list of output
    /// devices.</param>
    /// <returns>The name of the audio device at the requested index, or <c>NULL</c> on error.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetAudioDeviceName", CallingConvention = CallingConvention.Cdecl)]
    internal static extern string GetAudioDeviceName(int index, int isCapture);

    /// <summary>
    /// Get the preferred audio format of a specific audio device. The values returned by this function reflect the
    /// latest call to <see cref="GetNumAudioDevices"/>. That function must be called again to redetect available
    /// hardware.
    /// </summary>
    /// <param name="index">The index of the audio device. Valid values range from 0 to <see cref="GetNumAudioDevices"/>
    /// - 1.</param>
    /// <param name="isCapture">Non-zero to query the list of recording devices or zero to query the list of output
    /// devices.</param>
    /// <param name="spec">The <see cref="SdlAudioSpec"/> to be initialized by this function.</param>
    /// <returns>0 on success, non-zero on error.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetAudioDeviceSpec", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int GetAudioDeviceSpec(int index, int isCapture, out SdlAudioSpec spec);

    /// <summary>
    /// Get the name and preferred format of the default audio device.
    /// </summary>
    /// <param name="name">A pointer to be filled with the name of the default device (can be <c>NULL</c>). This must
    /// be freed with <see cref="SdlMemory.Free"/>.</param>
    /// <param name="spec">The <see cref="SdlAudioSpec"/> to be initialized by this function.</param>
    /// <param name="isCapture">Non-zero to query the default recording device or zero to query the default output
    /// device.</param>
    /// <returns>0 on success, non-zero on error.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetDefaultAudioInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern int GetDefaultAudioInfo(out IntPtr name, out SdlAudioSpec spec, int isCapture);

    /// <summary>
    /// Gets the number of built-in audio devices.
    /// </summary>
    /// <param name="isCapture">0 to request playback devices, or non-zero to request recording devices.</param>
    /// <returns>The number of available devices exposed by the current driver or -1 if an explicit list of devices
    /// can't be determined. A return value of -1 does not necessarily mean an error condition.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_GetNumAudioDevices", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int GetNumAudioDevices(int isCapture);

    /// <summary>
    /// Open a specific audio device. Passing in a device name of NULL requests the most reasonable default (and is
    /// equivalent to calling SDL_OpenAudio).
    /// </summary>
    /// <param name="device">The name of the device to open, as reported SDL_GetAudioDriverName(). Some drivers allow
    /// arbitrary and driver-specific strings, such as a hostname/IP address for a remote audio server, or a filename
    /// in the disk audio driver.</param>
    /// <param name="isCapture">Non-zero to specify a device should be opened for recording, not playback.</param>
    /// <param name="desired">An <see cref="SdlAudioSpec"/> structure representing the desired output format.</param>
    /// <param name="obtained">An <see cref="SdlAudioSpec"/> structure filled in with the actual output format.</param>
    /// <param name="allowedChanges">If this is <c>true</c>, the <paramref name="obtained"/> <see cref="SdlAudioSpec"/> is
    /// allowed to differ from the <paramref name="desired"/> specs.</param>
    /// <returns>A non-zero value on success or zero on failure. This will never return 1, since SDL reserves that ID
    /// for the SDL_OpenAudio() function.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_OpenAudioDevice", CallingConvention = CallingConvention.Cdecl)]
    private static extern uint OpenAudioDevice(IntPtr device, int isCapture, ref SdlAudioSpec desired, out SdlAudioSpec obtained, AllowedAudioSpecChanges allowedChanges);

    /// <summary>
    /// Open a specific audio device. Passing in a device name of NULL requests the most reasonable default (and is
    /// equivalent to calling SDL_OpenAudio).
    /// </summary>
    /// <param name="device">The name of the device to open, as reported SDL_GetAudioDriverName(). Some drivers allow
    /// arbitrary and driver-specific strings, such as a hostname/IP address for a remote audio server, or a filename
    /// in the disk audio driver.</param>
    /// <param name="isCapture">Non-zero to specify a device should be opened for recording, not playback.</param>
    /// <param name="desired">An <see cref="SdlAudioSpec"/> structure representing the desired output format.</param>
    /// <param name="obtained">An <see cref="SdlAudioSpec"/> structure filled in with the actual output format.</param>
    /// <param name="allowedChanges">If this is <c>true</c>, the <paramref name="obtained"/> <see cref="SdlAudioSpec"/> is
    /// allowed to differ from the <paramref name="desired"/> specs.</param>
    /// <returns>A non-zero value on success or zero on failure. This will never return 1, since SDL reserves that ID
    /// for the SDL_OpenAudio() function.</returns>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_OpenAudioDevice", CallingConvention = CallingConvention.Cdecl)]
    private static extern uint OpenAudioDevice(string device, int isCapture, ref SdlAudioSpec desired, out SdlAudioSpec obtained, AllowedAudioSpecChanges allowedChanges);

    /// <summary>
    /// Pause and unpause audio callback processing.
    /// </summary>
    /// <param name="dev">The audio device to change.</param>
    /// <param name="pauseOn">The <see cref="AudioState"/> to switch to.</param>
    [DllImport(Lib.Sdl2, EntryPoint = "SDL_PauseAudioDevice", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void PauseAudioDevice(uint dev, AudioState pauseOn);

    /// <summary>
    /// Get the name and preferred format of the default audio device.
    /// </summary>
    /// <param name="name">The returned name of the audio device.</param>
    /// <param name="spec">The <see cref="SdlAudioSpec"/> to be initialized by this function.</param>
    /// <param name="isCapture">Non-zero to query the default recording device or zero to query the default output
    /// device.</param>
    /// <returns>0 on success, non-zero on error.</returns>
    internal static int GetDefaultAudioInfo(out string? name, out SdlAudioSpec spec, int isCapture) {
        int result;
        if((result = GetDefaultAudioInfo(out IntPtr namePtr, out spec, isCapture)) != 0) {
            name = null;
            return result;
        }

        if(namePtr == IntPtr.Zero) {
            name = null;
        } else {
            name = Marshal.PtrToStringUTF8(namePtr);
            SdlMemory.Free(namePtr);
        }

        return result;
    }

    /// <summary>
    /// Open a specific audio device. Passing in a device name of NULL requests the most reasonable default (and is
    /// equivalent to calling SDL_OpenAudio).
    /// </summary>
    /// <param name="device">The name of the device to open, as reported SDL_GetAudioDriverName(). Some drivers allow
    /// arbitrary and driver-specific strings, such as a hostname/IP address for a remote audio server, or a filename
    /// in the disk audio driver. If this is <c>NULL</c>, the default audio device will be used.</param>
    /// <param name="isCapture">Non-zero to specify a device should be opened for recording, not playback.</param>
    /// <param name="desired">An <see cref="SdlAudioSpec"/> structure representing the desired output format.</param>
    /// <param name="obtained">An <see cref="SdlAudioSpec"/> structure filled in with the actual output format.</param>
    /// <param name="allowedChanges">If this is <c>true</c>, the <paramref name="obtained"/> <see cref="SdlAudioSpec"/> is
    /// allowed to differ from the <paramref name="desired"/> specs.</param>
    /// <returns>A non-zero value on success or zero on failure. This will never return 1, since SDL reserves that ID
    /// for the SDL_OpenAudio() function.</returns>
    internal static uint OpenAudioDeviceManaged(string? device, int isCapture, ref SdlAudioSpec desired, out SdlAudioSpec obtained, AllowedAudioSpecChanges allowedChanges) {
        return device is null
            ? OpenAudioDevice(IntPtr.Zero, 0, ref desired, out obtained, AllowedAudioSpecChanges.AllowAnyChange)
            : OpenAudioDevice(device, 0, ref desired, out obtained, AllowedAudioSpecChanges.AllowAnyChange);
    }
}
