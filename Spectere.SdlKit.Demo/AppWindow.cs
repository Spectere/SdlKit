using Spectere.SdlKit.EventArgs.Gamepad;
using Spectere.SdlKit.EventArgs.Keyboard;
using Spectere.SdlKit.EventArgs.Mouse;
using Spectere.SdlKit.EventArgs.Window;
using Spectere.SdlKit.Renderables;
using Spectere.SdlKit.SdlEvents.GameController;
using Spectere.SdlKit.SdlEvents.Keyboard;

namespace Spectere.SdlKit.Demo;

// The application window should inherit the Spectere.SdlKit.Window class.
public class AppWindow : Window {
    private const int DemoWidth = 40;
    private const int DemoHeight = 30;

    private int _drawState;
    private readonly decimal _scale;
    private readonly Image _randomPixelsImage;
    private readonly Image _colorOverlayImage;
    private readonly Image _paintSurfaceImage;
    private readonly Image _fontImage;

    private readonly List<Gamepad> _gamepads = [];

    // The window's constructor can perform any initialization tasks that it needs to perform, but it must also
    // call the base constructor to set up the SDL window and renderer.
    public AppWindow(decimal scale) : base(
        renderWidth: DemoWidth,
        renderHeight: DemoHeight,
        windowWidth: (int)(DemoWidth * scale),
        windowHeight: (int)(DemoHeight * scale),
        refreshRate: 60,
        title: "SDLKit Demo",
        fullscreen: false
    ) {
        // Create a separate "game" timer. This is used to process events and perform any non-rendering logic.
        AddTimer(GameUpdate, HertzToNanoseconds(60));
        
        // Register event handlers.
        OnKeyDown += KeyDown;
        OnKeyUp += KeyUp;
        OnGamepadAxisMotion += GamepadAxisMotion;
        OnGamepadButtonDown += GamepadButtonEvent;
        OnGamepadButtonUp += GamepadButtonEvent;
        OnGamepadDeviceAdded += GamepadDeviceEvent;
        OnGamepadDeviceRemapped += GamepadDeviceEvent;
        OnGamepadDeviceRemoved += GamepadDeviceEvent;
        OnGamepadSensorUpdate += GamepadSensorUpdate;
        OnGamepadTouchpadDown += GamepadTouchpadEvent;
        OnGamepadTouchpadMotion += GamepadTouchpadEvent;
        OnGamepadTouchpadUp += GamepadTouchpadEvent;
        OnMouseButtonDown += MouseButtonDown;
        OnMouseButtonUp += MouseButtonUp;
        OnMouseMotion += MouseMotion;
        OnMouseWheelMoved += MouseWheelMoved;
        OnWindowClose += WindowClose;

        // Enable the gamepad subsystem.
        Gamepad.EnableSubsystem();

        //
        // Testing BS :D
        //
        _scale = scale;
        
        _randomPixelsImage = new Image(this, ImageType.Streaming, DemoWidth, DemoHeight);
        _randomPixelsImage.ZOrder = 0;
        AddRenderable(_randomPixelsImage);

        _colorOverlayImage = new Image(this, ImageType.Static, DemoWidth / 2, DemoHeight / 2, TextureFiltering.Linear);
        _colorOverlayImage.BlendMode = BlendMode.Alpha;
        _colorOverlayImage.Destination = new SdlRect(0, 0, DemoWidth, DemoHeight);
        _colorOverlayImage.ZOrder = 1;
        for(var i = 0; i < _colorOverlayImage.Pixels.ByColor.Length; i++) {
            _colorOverlayImage.Pixels.ByColor[i].A = 64;
        }

        AddRenderable(_colorOverlayImage);

        var diamondTarget = new RenderTarget(this, 4, 4, TextureFiltering.Linear);
        diamondTarget.Destination = new SdlRect(18, 10, 8, 8);
        diamondTarget.ZOrder = 2;
        diamondTarget.RotationAngle = 45.0d;
        AddRenderable(diamondTarget);

        var diamondImage = new Image(this, ImageType.Streaming, 4, 4);
        diamondImage.ZOrder = 0;
        diamondTarget.AddRenderable(diamondImage);
        for(var i = 0; i < diamondImage.Pixels.ByColor.Length; i++) {
            diamondImage.Pixels.ByColor[i].R = (byte)_rng.Next(0, 64);
            diamondImage.Pixels.ByColor[i].G = (byte)_rng.Next(0, 64);
            diamondImage.Pixels.ByColor[i].B = (byte)_rng.Next(0, 64);
        }
        diamondImage.Update(null);

        _paintSurfaceImage = new Image(this, ImageType.Streaming, DemoWidth, DemoHeight);
        _paintSurfaceImage.ZOrder = 10;
        _paintSurfaceImage.BlendMode = BlendMode.Alpha;
        AddRenderable(_paintSurfaceImage);

        _fontImage = Image.FromFile(this, "Assets/SpectereFont-8x16.png");
        _fontImage.ZOrder = 100;
        _fontImage.Window = new SdlRect(16, 3, 8, 10);
        _fontImage.Destination = new SdlRect(0, 0, 8, 10);
        _fontImage.BlendMode = BlendMode.Alpha;
        AddRenderable(_fontImage);
    }

    private bool _upPress;
    private bool _downPress;
    private bool _leftPress;
    private bool _rightPress;
    private void KeyDown(object? sender, KeyEventArgs e) {
        switch(e.KeyCode) {
            case Keycode.Escape:
                Shutdown();
                break;
            
            case Keycode.Up:
                _upPress = true;
                break;
            
            case Keycode.Down:
                _downPress = true;
                break;
            
            case Keycode.Left:
                _leftPress = true;
                break;
            
            case Keycode.Right:
                _rightPress = true;
                break;
        }
        
        Console.WriteLine($"Key Down: {e.KeyCode}");
    }

    private void KeyUp(object? sender, KeyEventArgs e) {
        switch(e.KeyCode) {
            case Keycode.Up:
                _upPress = false;
                break;
            
            case Keycode.Down:
                _downPress = false;
                break;
            
            case Keycode.Left:
                _leftPress = false;
                break;
            
            case Keycode.Right:
                _rightPress = false;
                break;
        }
        
        Console.WriteLine($"Key Up  : {e.KeyCode}");
    }

    private ushort _lowFreqRumble;
    private ushort _highFreqRumble;
    private void GamepadAxisMotion(object? sender, GamepadAxisEventArgs e) {
        Console.WriteLine($"Gamepad ID: {e.DeviceId} | Axis: {e.Axis} | Value: {e.Position}");
        if(e.Axis is not (GameControllerAxis.TriggerLeft or GameControllerAxis.TriggerRight)) {
            Console.WriteLine("Aw shucks.");
            return;
        }

        var adjusted = (ushort)(e.Position > 250 ? e.Position * 2 : 0);
        
        switch(e) {
            case { Axis: GameControllerAxis.TriggerLeft }:
                _lowFreqRumble = adjusted;
                break;
            case { Axis: GameControllerAxis.TriggerRight }:
                _highFreqRumble = adjusted;
                break;
        }

        if(_lowFreqRumble != 0 || _highFreqRumble != 0) return;
        foreach(var gamepad in _gamepads) {
            gamepad.Rumble(0, 0, 1000);
        }
    }

    private void GamepadButtonEvent(object? sender, GamepadButtonEventArgs e) {
        Console.WriteLine($"Gamepad ID: {e.DeviceId} | Button: {e.Button} | State: {e.State}");
    }
    
    private void GamepadDeviceEvent(object? sender, GamepadDeviceEventArgs e) {
        Console.WriteLine($"Gamepad ID: {e.DeviceId} | Device Event: {e.EventType}");

        switch(e.EventType) {
            case GamepadDeviceEventType.GamepadDeviceAdded: {
                var gamepad = Gamepad.Open(e.DeviceId);
                if(gamepad is null) {
                    return;
                }

                Console.WriteLine($"        Name: {gamepad.Name}");
                Console.WriteLine($"        Type: {gamepad.Type}");
                Console.WriteLine($"        Supports LED color changes: {gamepad.SupportsLedColorChange}");
                Console.WriteLine($"        Supports rumble: {gamepad.SupportsRumble}");
                Console.WriteLine($"        Supports rumble triggers: {gamepad.SupportsRumbleTriggers}");
                Console.WriteLine($"        Touchpad count: {gamepad.TouchpadCount}");
                _gamepads.Add(gamepad);
                break;
            }
            case GamepadDeviceEventType.GamepadDeviceRemoved: {
                var gamepad = _gamepads.FirstOrDefault(x => x.DeviceId == e.DeviceId);
                if(gamepad is null) {
                    return;
                }
                gamepad.Close();
                _gamepads.Remove(gamepad);
                break;
            }
        }
    }

    private void GamepadSensorUpdate(object? sender, GamepadSensorUpdateEventArgs e) {
        Console.WriteLine($"Gamepad ID: {e.DeviceId} | Sensor: {e.Sensor} | X: {e.X} | Y: {e.Y} | Z: {e.Z}");
    }

    private void GamepadTouchpadEvent(object? sender, GamepadTouchpadEventArgs e) {
        Console.WriteLine($"Gamepad ID: {e.DeviceId} | Event: {e.EventType} | Finger: {e.Finger} | X: {e.X} | Y: {e.Y} | P: {e.Pressure}");
    }

    private void MouseButtonDown(object? sender, MouseButtonEventArgs e) {
        _drawState = (int)e.Button;
    }

    private void MouseButtonUp(object? sender, MouseButtonEventArgs e) {
        _drawState = 0;
    }
    
    private void MouseMotion(object? sender, MouseMotionEventArgs e) {
        if(_drawState == 0 || _drawState >= 4) {
            return;
        }

        var pixelX = Convert.ToInt32(Math.Floor(e.PointerX / _scale));
        var pixelY = Convert.ToInt32(Math.Floor(e.PointerY / _scale));

        if(_drawState == 1) {
            _paintSurfaceImage.Pixels.ByColor[pixelY * DemoWidth + pixelX].R = 255;
            _paintSurfaceImage.Pixels.ByColor[pixelY * DemoWidth + pixelX].G = 255;
            _paintSurfaceImage.Pixels.ByColor[pixelY * DemoWidth + pixelX].B = 255;
            _paintSurfaceImage.Pixels.ByColor[pixelY * DemoWidth + pixelX].A = 255;
        } else if(_drawState == 3) {
            _paintSurfaceImage.Pixels.ByColor[pixelY * DemoWidth + pixelX].R = 0;
            _paintSurfaceImage.Pixels.ByColor[pixelY * DemoWidth + pixelX].G = 0;
            _paintSurfaceImage.Pixels.ByColor[pixelY * DemoWidth + pixelX].B = 0;
            _paintSurfaceImage.Pixels.ByColor[pixelY * DemoWidth + pixelX].A = 255;
        } else {
            _paintSurfaceImage.Pixels.ByColor[pixelY * DemoWidth + pixelX].A = 0;
        }
        
        _paintSurfaceImage.Update(new SdlRect(pixelX, pixelY, 1, 1));
    }

    private void MouseWheelMoved(object? sender, MouseWheelEventArgs e) {
        Console.WriteLine($"PointerX: {e.PointerX} " +
                          $"| PointerY: {e.PointerY} " +
                          $"| H: {e.HorizontalDelta} " +
                          $"| V: {e.VerticalDelta} " +
                          $"| HP: {e.HorizontalDeltaPrecise} " +
                          $"| VP: {e.VerticalDeltaPrecise}");
    }

    private void WindowClose(object? sender, WindowCloseEventArgs e) {
        Shutdown();
    }

    private void Shutdown() {
        foreach(var gamepad in _gamepads) {
            Console.WriteLine($"Closing controller #{gamepad.DeviceId}: {gamepad.Name}");
            gamepad.Close();
        }
        
        Stop();
    }

    //private int _counter;
    private int _playerX = 0;
    private int _playerY = 0;

    private void UpdateTitle() {
        Title = $"SDLKit Demo - ({_playerX}, {_playerY})";
    }

    private void ClampPlayerSpriteLocation() {
        if(_playerX < 0) {
            _playerX = 0;
        }

        if(_playerX > DemoWidth - 8) {
            _playerX = DemoWidth - 8;
        }

        if(_playerY < 0) {
            _playerY = 0;
        }

        if(_playerY > DemoHeight - 10) {
            _playerY = DemoHeight - 10;
        }
    }
    
    private void GameUpdate(long delta) {
        //Console.WriteLine($"Game  - {_counter++}");
        ProcessEvents();

        if(_fontImage.Destination is not null) {
            if(_upPress) {
                _playerY -= 1;
            } else if(_downPress) {
                _playerY += 1;
            }
            
            if(_leftPress) {
                _playerX -= 1;
            } else if(_rightPress) {
                _playerX += 1;
            }
        }

        ClampPlayerSpriteLocation();
        _fontImage.Destination = new SdlRect(_playerX, _playerY, 8, 10);
        UpdateTitle();
        
        // Let's get ready to rumble!!
        if(_lowFreqRumble == 0 && _highFreqRumble == 0) return;
        foreach(var gamepad in _gamepads) {
            gamepad.Rumble(_lowFreqRumble, _highFreqRumble, uint.MaxValue);
        }
    }

    private int _pixelIdx;
    private int _overlayIdx = -NumPixels / 4;
    private int _overlayStage;
    private const int NumPixels = DemoWidth * DemoHeight;
    private readonly Random _rng = new();
    protected override void VideoPreRender(long delta) {
        //Console.WriteLine($"Video - {_counter++}");

        _randomPixelsImage.Pixels.ByColor[_pixelIdx].R = (byte)_rng.Next(0, 255);
        _randomPixelsImage.Pixels.ByColor[_pixelIdx].G = (byte)_rng.Next(0, 255);
        _randomPixelsImage.Pixels.ByColor[_pixelIdx].B = (byte)_rng.Next(0, 255);

        if(_overlayIdx >= 0) {
            switch(_overlayStage) {
                case 0:
                    _colorOverlayImage.Pixels.ByColor[_overlayIdx].R = 255;
                    break;
                case 1:
                    _colorOverlayImage.Pixels.ByColor[_overlayIdx].G = 255;
                    break;
                case 2:
                    _colorOverlayImage.Pixels.ByColor[_overlayIdx].B = 255;
                    break;
                case 3:
                    _colorOverlayImage.Pixels.ByColor[_overlayIdx].R = 0;
                    break;
                case 4:
                    _colorOverlayImage.Pixels.ByColor[_overlayIdx].G = 0;
                    break;
                case 5:
                    _colorOverlayImage.Pixels.ByColor[_overlayIdx].B = 0;
                    break;
            }
        }

        if(++_pixelIdx >= NumPixels) {
            _pixelIdx = 0;
        }

        if(++_overlayIdx >= NumPixels / 4) {
            _overlayIdx = 0;
            _overlayStage = _overlayStage == 5 ? 0 : _overlayStage + 1;
        }
        
        _randomPixelsImage.Update(new SdlRect(0, 0, DemoWidth, DemoHeight));
        _colorOverlayImage.Update(new SdlRect(0, 0, DemoWidth, DemoHeight));
    }
}
