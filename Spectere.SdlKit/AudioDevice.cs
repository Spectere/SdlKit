namespace Spectere.SdlKit;

/// <summary>
/// Represents an audio device on the system.
/// </summary>
/// <param name="Name">The system-defined name of the audio device.</param>
/// <param name="PreferredSpec">The preferred <see cref="AudioSpec"/> for this device. While these settings are
/// recommended in many cases, this can be ignored if a different specification is required or requested.</param>
public record AudioDevice(string Name, AudioSpec PreferredSpec);
