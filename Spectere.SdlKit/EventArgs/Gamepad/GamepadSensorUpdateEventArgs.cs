using Spectere.SdlKit.SdlEvents;
using Spectere.SdlKit.SdlEvents.GameController;

namespace Spectere.SdlKit.EventArgs.Gamepad;

/// <summary>
/// Represents the event arguments for gamepad sensor events.
/// </summary>
public class GamepadSensorUpdateEventArgs : CommonEventArgs<GameControllerSensorEvent> {
    /// <summary>
    /// The identifier of the device that generated this event.
    /// </summary>
    public int DeviceId => SdlEvent.Which;

    /// <summary>
    /// The type of sensor that's being updated.
    /// </summary>
    public SensorType Sensor => SdlEvent.Sensor;

    /// <summary>
    /// The X value of the sensor, represented as a floating point value.
    /// </summary>
    public float X => SdlEvent.Data1;

    /// <summary>
    /// The Y value of the sensor, represented as a floating point value.
    /// </summary>
    public float Y => SdlEvent.Data2;

    /// <summary>
    /// The Z value of the sensor, represented as a floating point value.
    /// </summary>
    public float Z => SdlEvent.Data3;
}
