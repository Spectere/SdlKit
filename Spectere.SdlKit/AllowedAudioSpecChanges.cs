namespace Spectere.SdlKit; 

/// <summary>
/// Specifies what should happen if SDL is unable to open an audio device with the requested specifications.
/// If any changes are allowed, you must be sure to check the CurrentAudioSpec property before writing any audio
/// to the buffer to ensure that the application is sending audio to SdlKit in the correct format.
/// </summary>
[Flags]
public enum AllowedAudioSpecChanges {
    /// <summary>Do not allow any audio spec changes.</summary>
    DisallowChange = 0x00,

    /// <summary>Allow frequency changes.</summary>
    AllowFrequencyChange = 0x01,

    /// <summary>Allow the audio format (bit size, data type, etc.) to change.</summary>
    AllowFormatChange = 0x02,

    /// <summary>Allow the number of channels to change.</summary>
    AllowChannelsChange = 0x04,

    /// <summary>Allow any detail in the audio spec to change.</summary>
    AllowAnyChange = AllowFrequencyChange | AllowFormatChange | AllowChannelsChange
}
