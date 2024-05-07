using Spectere.SdlKit.EventArgs.Gamepad;
using Spectere.SdlKit.EventArgs.Keyboard;
using Spectere.SdlKit.EventArgs.Mouse;
using Spectere.SdlKit.EventArgs.Window;
using Spectere.SdlKit.Renderables;
using Glyph = Spectere.SdlKit.Renderables.TextConsole.Glyph;
using Spectere.SdlKit.SdlEvents.GameController;
using Spectere.SdlKit.SdlEvents.Keyboard;
using System.Text;

namespace Spectere.SdlKit.Demo;

// The application window should inherit the Spectere.SdlKit.Window class.
public class AppWindow : Window {
    private const int TargetPixelWidth = 400;
    private const int TargetPixelHeight = 300;

    private const int MouseSurfaceWidth = 80;
    private const int MouseSurfaceHeight = 60;
    private const int MouseSurfacePixels = MouseSurfaceWidth * MouseSurfaceHeight;
    private const decimal MouseSurfaceScale = (decimal)MouseSurfaceWidth / TargetPixelWidth;
    
    private const int PixelBlobWidth = 40;
    private const int PixelBlobHeight = 30;

    private int _drawState;
    private readonly decimal _scale;
    private readonly Image _randomPixelsImage;
    private readonly Image _colorOverlayImage;
    private readonly Image _paintSurfaceImage;
    private readonly Image _fontImage;
    private readonly TextConsole _audioConsole;
    private readonly TileGrid _tileGrid;

    private readonly List<Gamepad> _gamepads = [];

    private readonly Audio _audio;

    // The window's constructor can perform any initialization tasks that it needs to perform, but it must also
    // call the base constructor to set up the SDL window and renderer.
    public AppWindow(decimal scale) : base(
        renderWidth: TargetPixelWidth,
        renderHeight: TargetPixelHeight,
        windowWidth: (int)(TargetPixelWidth * scale),
        windowHeight: (int)(TargetPixelHeight * scale),
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
        
        _randomPixelsImage = new Image(this, ImageType.Streaming, PixelBlobWidth, PixelBlobHeight);
        _randomPixelsImage.Destination = new SdlRect(0, 0, TargetPixelWidth, TargetPixelHeight);
        _randomPixelsImage.ZOrder = 0;
        AddRenderable(_randomPixelsImage);

        _colorOverlayImage = new Image(this, ImageType.Static, PixelBlobWidth / 2, PixelBlobHeight / 2, TextureFiltering.Linear);
        _colorOverlayImage.BlendMode = BlendMode.Alpha;
        _colorOverlayImage.Destination = new SdlRect(0, 0, TargetPixelWidth, TargetPixelHeight);
        _colorOverlayImage.ZOrder = 1;
        for(var i = 0; i < _colorOverlayImage.Pixels.ByColor.Length; i++) {
            _colorOverlayImage.Pixels.ByColor[i].A = 64;
        }

        AddRenderable(_colorOverlayImage);

        var diamondTarget = new RenderTarget(this, 4, 4, TextureFiltering.Linear);
        diamondTarget.Destination = new SdlRect(198, 94, 80, 80);
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

        _paintSurfaceImage = new Image(this, ImageType.Streaming, MouseSurfaceWidth, MouseSurfaceHeight);
        _paintSurfaceImage.BlendMode = BlendMode.Alpha;
        _paintSurfaceImage.Destination = new SdlRect(0, 0, TargetPixelWidth, TargetPixelHeight);
        _paintSurfaceImage.ZOrder = 10;
        AddRenderable(_paintSurfaceImage);

        _fontImage = Image.FromFile(this, "Assets/SpectereFont-8x16.png");
        _fontImage.ZOrder = 100;
        _fontImage.Window = new SdlRect(16, 3, 8, 10);
        _fontImage.Destination = new SdlRect(0, 0, 40, 50);
        _fontImage.BlendMode = BlendMode.Alpha;
        AddRenderable(_fontImage);

        var testConsoleWidth = TargetPixelWidth / 2;
        var testConsoleHeight = TargetPixelHeight / 2;
        var smileConsole = new TextConsole(this, testConsoleWidth, testConsoleHeight,
            "Assets/SpectereFont-8x16.png", 8, 16);
        smileConsole.ZOrder = 8;
        smileConsole.DefaultGlyph = new Glyph {
            GlyphIndex = ' ',
            ForegroundColor = new SdlColor(192, 192, 192),
            BackgroundColor = new SdlColor(0, 0, 192)
        };
        smileConsole.Destination = new SdlRect(0, 150, 200, 150);
        smileConsole.PaddingColor = new SdlColor(64, 64, 255);
        smileConsole.GlyphPadding = new Padding(1, 0);
        smileConsole.ConsolePadding = new Padding(2, 0);
        smileConsole.CenterTextArea();
        smileConsole.Clear();
        for(var y = 0; y < smileConsole.ConsoleHeight; y++)
        for(var x = 0; x < smileConsole.ConsoleWidth; x++) {
            smileConsole.SetCell(
                x, y, _rng.Next(1, 3),
                new SdlColor((byte)_rng.Next(0, 256), (byte)_rng.Next(0, 256), (byte)_rng.Next(0, 256)),
                null
            );
        }
        AddRenderable(smileConsole);

        var textConsole = new TextConsole(this, testConsoleWidth, testConsoleHeight,
            "Assets/SpectereFont-8x16.png", 8, 16);
        textConsole.ZOrder = 1000;
        textConsole.Destination = new SdlRect(200, 150, 200, 150);
        textConsole.BlendMode = BlendMode.Alpha;
        textConsole.DefaultGlyph = new Glyph {
            GlyphIndex = ' ',
            ForegroundColor = new SdlColor(192, 192, 192),
            BackgroundColor = new SdlColor(0, 0, 0, 192)
        };
        textConsole.CenterTextArea();
        textConsole.Clear();
        textConsole.WriteLine("Line 1...");
        textConsole.WriteLine("  Line 2...");
        textConsole.WriteLine("    Line 3...");
        textConsole.WriteLine("  Line 4...");
        textConsole.WriteLine("Line 5...");
        textConsole.WriteLine("  Line 6...");
        textConsole.WriteLine("    Line 7...");
        textConsole.WriteLine("  Line 8...");
        textConsole.WriteLine("Line 9...");
        textConsole.WriteLine("  Line A...");
        textConsole.Write("lmao \x01\b\x03\rlove");
        AddRenderable(textConsole);

        _audioConsole = new TextConsole(this, 14 * 16 + 4, 32 + 4,
            "Assets/SpectereFont-16x32.png", 16, 32);
        _audioConsole.ZOrder = 2000;
        _audioConsole.Destination = new SdlRect(TargetPixelWidth - 14 * 16 - 4, 0, 14 * 16 + 4, 32 + 4);
        _audioConsole.BlendMode = BlendMode.Alpha;
        _audioConsole.PaddingColor = new SdlColor(0, 128, 192, 192);
        _audioConsole.DefaultGlyph = new Glyph
        {
            GlyphIndex = ' ',
            ForegroundColor = new SdlColor(192, 192, 192),
            BackgroundColor = new SdlColor(0, 0, 0, 192)
        };
        _audioConsole.CenterTextArea();
        _audioConsole.Clear();
        _audioConsole.AutoScroll = false;
        _audioConsole.Write("audio:", new SdlColor(255, 255, 255));
        AddRenderable(_audioConsole);

        _tileGrid = new TileGrid(this, 6, 3, "Assets/TileGridDemo-16x16.png", 16, 16);
        _tileGrid.ZOrder = 9;
        _tileGrid.Destination = new SdlRect(0, TargetPixelHeight / 2 - _tileGrid.Height, _tileGrid.Width, _tileGrid.Height);
        _tileGrid.BackgroundColor = new SdlColor(SkyR, SkyG, SkyB);
        _tileGrid.SetTiles(0, [
            -1, -1, -1,  3,  4, -1,
            -1, -1,  3,  1,  2,  0,
             0,  0,  1,  5,  5,  5
        ]);
        _tileGrid.AddLayer([
            -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1,  6,
            -1,  7, -1, -1, -1, -1
        ]);
        AddRenderable(_tileGrid);
        
        // Initialize audio.
        _audio = new Audio();
        _audio.AudioRequested += AudioRequested;
        
        _audio.Open();
        _sampleRate = _audio.CurrentAudioSpec?.Frequency ?? 44100;

        var spec = _audio.CurrentAudioSpec;
        if(_audio.Opened && spec is not null) {
            var sb = new StringBuilder();
            sb.Append("Audio opened: ");
            sb.Append($"{spec.Frequency} hz, ");
            sb.Append($"{spec.BitSize}-bit, ");
            sb.Append(spec.IsFloat
                ? "floating-point"
                : spec.IsSigned ? "signed integer" : "unsigned integer"
            );
            sb.Append($" (buffer size: {spec.Samples} samples)");
            
            Console.WriteLine(sb.ToString());
        } else if (_audio.Opened && spec is null) {
            Console.WriteLine("Audio is open but the spec is null? wtf?");
        } else {
            Console.WriteLine("Audio is sad :(");
        }
    }
    
    private bool _upPress;
    private bool _downPress;
    private bool _leftPress;
    private bool _rightPress;
    private bool _hzUpPress;
    private bool _hzDownPress;
    private void KeyDown(object? sender, KeyEventArgs e) {
        if(e.Repeat) {
            return;
        }
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
            
            case Keycode.RightBracket:
                _hzUpPress = true;
                break;
            
            case Keycode.LeftBracket:
                _hzDownPress = true;
                break;
            
            case Keycode.Backslash:
                if(!_audio.Opened) {
                    return;
                }
                _audioMuted = !_audioMuted;
                _audio.SetPlaybackState(_audioMuted ? AudioState.Paused : AudioState.Unpaused);
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
            
            case Keycode.RightBracket:
                _hzUpPress = false;
                break;
            
            case Keycode.LeftBracket:
                _hzDownPress = false;
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
        if(_drawState is 0 or >= 4) {
            return;
        }

        var pixelX = Convert.ToInt32(Math.Floor(e.PointerX * MouseSurfaceScale / _scale));
        var pixelY = Convert.ToInt32(Math.Floor(e.PointerY * MouseSurfaceScale / _scale));
        var pixelIdx = pixelY * MouseSurfaceWidth + pixelX;
        if(pixelIdx is < 0 or >= MouseSurfacePixels
           || pixelX is < 0 or >= MouseSurfaceWidth
           || pixelY is < 0 or MouseSurfaceHeight) {
            return;
        }

        if(_drawState == 1) {
            _paintSurfaceImage.Pixels.ByColor[pixelIdx].R = 255;
            _paintSurfaceImage.Pixels.ByColor[pixelIdx].G = 255;
            _paintSurfaceImage.Pixels.ByColor[pixelIdx].B = 255;
            _paintSurfaceImage.Pixels.ByColor[pixelIdx].A = 255;
        } else if(_drawState == 3) {
            _paintSurfaceImage.Pixels.ByColor[pixelIdx].R = 0;
            _paintSurfaceImage.Pixels.ByColor[pixelIdx].G = 0;
            _paintSurfaceImage.Pixels.ByColor[pixelIdx].B = 0;
            _paintSurfaceImage.Pixels.ByColor[pixelIdx].A = 255;
        } else {
            _paintSurfaceImage.Pixels.ByColor[pixelIdx].A = 0;
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
    private int _playerX;
    private int _playerY;

    private void UpdateTitle() {
        Title = $"SDLKit Demo - ({_playerX}, {_playerY})";
    }

    private void ClampPlayerSpriteLocation() {
        if(_playerX < 0) {
            _playerX = 0;
        }

        if(_playerX > TargetPixelWidth - 40) {
            _playerX = TargetPixelWidth - 40;
        }

        if(_playerY < 0) {
            _playerY = 0;
        }

        if(_playerY > TargetPixelHeight - 50) {
            _playerY = TargetPixelHeight - 50;
        }
    }

    private bool _audioMuted = true;
    private int _audioHertz = 440;
    private readonly int _sampleRate;
    private int _period;
    private void AudioRequested(object sender, int samplesRequested, in float[] buffer) {
        for(var i = 0; i < buffer.Length; i++) {
            buffer[i] = (float)Math.Sin(Math.PI * _audioHertz * _period / _sampleRate);
            _period++;
        }
    }

    private const int AudioHertzDelta = 4;
    private const int AudioHertzMin = AudioHertzDelta;
    private const int AudioHertzMax = 44100;
    private const byte SkyR = 85;
    private const byte SkyG = 170;
    private const byte SkyB = 255;
    private byte _skyAdjust = 64;
    private bool _skyAdjustUp;
    private SdlColor _skyColor = new(SkyR, SkyG, SkyB);
    private bool _grassGUp;
    private SdlColor _grassColor = new(128, 255, 128);
    private void GameUpdate(long delta) {
        //Console.WriteLine($"Game  - {_counter++}");
        ProcessEvents();

        if(_fontImage.Destination is not null) {
            if(_upPress) {
                _playerY -= 4;
            } else if(_downPress) {
                _playerY += 4;
            }
            
            if(_leftPress) {
                _playerX -= 4;
            } else if(_rightPress) {
                _playerX += 4;
            }
        }

        if(!_audioMuted && !(_hzUpPress && _hzDownPress)) {
            if(_hzUpPress) {
                _audioHertz += AudioHertzDelta;
                if(_audioHertz > AudioHertzMax) {
                    _audioHertz = AudioHertzMax;
                }
            } else if(_hzDownPress) {
                _audioHertz -= AudioHertzDelta;
                if(_audioHertz < AudioHertzMin) {
                    _audioHertz = AudioHertzMin;
                }
            }
        }

        ClampPlayerSpriteLocation();
        _fontImage.Destination = new SdlRect(_playerX, _playerY, 40, 50);
        UpdateTitle();
        
        // Mess with the tile grid. :D
        if((_skyAdjust <= 0 && !_skyAdjustUp) || (_skyAdjust >= 64 && _skyAdjustUp)) {
            _skyAdjustUp = !_skyAdjustUp;
        }
        _skyAdjust += (byte)(_skyAdjustUp ? 2 : -2);
        _skyColor.R = (byte)(SkyR - _skyAdjust);
        _skyColor.G = (byte)(SkyG - _skyAdjust);
        _skyColor.B = (byte)(SkyB - _skyAdjust);
        _tileGrid.BackgroundColor = _skyColor;

        if((_grassColor.G <= 128 && !_grassGUp) || (_grassColor.G >= 255 && _grassGUp)) {
            _grassGUp = !_grassGUp;
        }
        _grassColor.G += (byte)(_grassGUp ? 4 : -4);
        _tileGrid.SetLayerColorModulation(0, _grassColor);
        
        // Let's get ready to rumble!!
        if(_lowFreqRumble == 0 && _highFreqRumble == 0) return;
        foreach(var gamepad in _gamepads) {
            gamepad.Rumble(_lowFreqRumble, _highFreqRumble, uint.MaxValue);
        }
    }

    private int _pixelIdx;
    private int _overlayIdx = -NumPixels / 4;
    private int _overlayStage;
    private const int NumPixels = PixelBlobWidth * PixelBlobHeight;
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

        _randomPixelsImage.Update(new SdlRect(0, 0, PixelBlobWidth, PixelBlobHeight));
        _colorOverlayImage.Update(new SdlRect(0, 0, PixelBlobWidth, PixelBlobHeight));

        // Update audio control.
        _audioConsole.SetCursorPosition(7, 0);

        if(_audioMuted) {
            _audioConsole.Write("muted  ", new SdlColor(255, 0, 0));
        } else {
            _audioConsole.Write("       ");
            _audioConsole.SetCursorPosition(7, 0);
            _audioConsole.Write($"{_audioHertz}hz", new SdlColor(0, 255, 0));
        }
    }
}
