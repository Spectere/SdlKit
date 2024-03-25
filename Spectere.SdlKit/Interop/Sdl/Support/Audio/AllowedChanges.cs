namespace Spectere.SdlKit.Interop.Sdl.Support.Audio; 

/// <summary>
/// Specifies how SDL should behave when a device cannot offer a specific feature. If
/// the application requests a feature that the hardware doesn't offer, SDL will always
/// try to get the closest equivalent.
/// </summary>
[Flags]
internal enum AllowedChanges {
    /// <summary>Do not allow any audio spec changes.</summary>
    DisallowChange = 0x00,

    /// <summary>Allow frequency changes.</summary>
    AllowFrequencyChange = 0x01,

    /// <summary>Allow the audio format to change.</summary>
    AllowFormatChange = 0x02,

    /// <summary>Allow the number of channels to change.</summary>
    AllowChannelsChange = 0x04,

    /// <summary>Allow any detail in the audio spec to change.</summary>
    AllowAnyChange = AllowFrequencyChange | AllowFormatChange | AllowChannelsChange
}
