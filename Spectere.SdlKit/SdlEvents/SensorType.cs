using Spectere.SdlKit.EventArgs.Gamepad;

namespace Spectere.SdlKit.SdlEvents;

/// <summary>
/// The different sensors defined by SDL. Additional sensors may be available, using platform-dependent semantics.
/// </summary>
public enum SensorType {
    /// <summary>
    /// An invalid sensor.
    /// </summary>
    Invalid = -1,
    
    /// <summary>
    /// An unknown sensor type.
    /// </summary>
    Unknown,
    
    /// <summary>
    /// <para>
    /// An accelerometer. Accelerometers report the current acceleration in SI meters per second squared. This
    /// measurement includes the force of gravity, so a device at rest will have a value of
    /// <see cref="GamepadConstants.StandardGravity"/> away from the center of the earth, which is a positive Y value.
    /// </para>
    /// <para>
    /// For game controllers held in front of the user, -X/+X are defined as left/right movement, -Y/+Y is down/up,
    /// and -Z/+Z is farther/closer.
    /// </para>
    /// </summary>
    Accelerometer,
    
    /// <summary>
    /// <para>
    /// A gyroscope. Gyroscopes return the current rate of rotation in radians per second. The rotation is positive in
    /// the counter-clockwise direction. That is, an observer looking from a positive location on one of the axes would
    /// see positive rotation on that axis when it appeared to be rotating counter-clockwise.
    /// </para>
    /// <para>
    /// For game controllers held in front of the user, -X/+X are defined as left/right rotation, -Y/+Y is down/up,
    /// and -Z/+Z is farther/closer.
    /// </para>
    /// </summary>
    Gyroscope,
    
    /// <summary>
    /// <para>
    /// The accelerometer for the left Nintendo Switch Joy-Con controller or Wii Nunchuk. Accelerometers report the
    /// current acceleration in SI meters per second squared. This measurement includes the force of gravity, so a
    /// device at rest will have a value of <see cref="GamepadConstants.StandardGravity"/> away from the center of the
    /// earth, which is a positive Y value.
    /// </para>
    /// <para>
    /// For game controllers held in front of the user, -X/+X are defined as left/right movement, -Y/+Y is down/up,
    /// and -Z/+Z is farther/closer.
    /// </para>
    /// </summary>
    AccelerometerLeft,
    
    /// <summary>
    /// <para>
    /// The gyroscope for the left Nintendo Switch Joy-Con controller. Gyroscopes return the current rate of rotation
    /// in radians per second. The rotation is positive in the counter-clockwise direction. That is, an observer
    /// looking from a positive location on one of the axes would see positive rotation on that axis when it appeared
    /// to be rotating counter-clockwise.
    /// </para>
    /// <para>
    /// For game controllers held in front of the user, -X/+X are defined as left/right rotation, -Y/+Y is down/up,
    /// and -Z/+Z is farther/closer.
    /// </para>
    /// </summary>
    GyroscopeLeft,
    
    /// <summary>
    /// <para>
    /// The accelerometer for the right Nintendo Switch Joy-Con controller. Accelerometers report the current
    /// acceleration in SI meters per second squared. This measurement includes the force of gravity, so a device at
    /// rest will have a value of <see cref="GamepadConstants.StandardGravity"/> away from the center of the earth,
    /// which is a positive Y value.
    /// </para>
    /// <para>
    /// For game controllers held in front of the user, -X/+X are defined as left/right movement, -Y/+Y is down/up,
    /// and -Z/+Z is farther/closer.
    /// </para>
    /// </summary>
    AccelerometerRight,
    
    /// <summary>
    /// <para>
    /// The gyroscope for the right Nintendo Switch Joy-Con controller. Gyroscopes return the current rate of rotation
    /// in radians per second. The rotation is positive in the counter-clockwise direction. That is, an observer
    /// looking from a positive location on one of the axes would see positive rotation on that axis when it appeared
    /// to be rotating counter-clockwise.
    /// </para>
    /// <para>
    /// For game controllers held in front of the user, -X/+X are defined as left/right rotation, -Y/+Y is down/up,
    /// and -Z/+Z is farther/closer.
    /// </para>
    /// </summary>
    GyroscopeRight
}
