using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Renderables;

namespace Spectere.SdlKit;

/// <summary>
/// An inheritable class that provides an SDL window to the application.
/// </summary>
public partial class Window : Timer {
    /// <summary>
    /// Gets or sets the title of this window.
    /// </summary>
    public string Title {
        get => _windowTitle;
        set {
            if(_sdlWindow.IsNull) {
                return;
            }
            
            _windowTitle = value;
            Video.SetWindowTitle(_sdlWindow, _windowTitle);
        }
    }
    private string _windowTitle;
    
    /// <summary>
    /// Gets the height of this window.
    /// </summary>
    public int WindowHeight => _windowHeight;
    private int _windowHeight;

    /// <summary>
    /// Gets the width of this window.
    /// </summary>
    public int WindowWidth => _windowWidth; 
    private int _windowWidth;

    /// <summary>
    /// Initializes a new <see cref="Window"/>. This will display a window on the screen that rendering layers
    /// can be added to.
    /// </summary>
    /// <param name="width">The width of the rendering surface and window.</param>
    /// <param name="height">The height of the rendering surface and window.</param>
    /// <param name="refreshRate">The frequency in which the video update routines should be called, in hertz.</param>
    /// <param name="title">The title of the window.</param>
    /// <param name="fullscreen">If this is <c>true</c>, the video output will be fullscreen. If this is <c>false</c>,
    /// it will be windowed.</param>
    /// <param name="textureFiltering">Sets the texture filtering method that will be used by this window's rendering
    /// surface. Note that this is only noticeable if the rendering surface is a different size from the window. This
    /// defaults to <see cref="TextureFiltering.Nearest"/>.</param>
    protected Window(int width, int height, int refreshRate, string title = "SDLKit Window", bool fullscreen = false, TextureFiltering textureFiltering = TextureFiltering.Nearest)
        : this(width, height, width, height, refreshRate, title, fullscreen, textureFiltering) { }

    /// <summary>
    /// Initializes a new <see cref="Window"/>. This will display a window on the screen that rendering layers
    /// can be added to.
    /// </summary>
    /// <param name="renderWidth">The width of the rendering surface.</param>
    /// <param name="renderHeight">The height of the rendering surface.</param>
    /// <param name="windowWidth">The width of the window. If this differs from <paramref name="renderWidth"/>,
    /// the video output will be scaled.</param>
    /// <param name="windowHeight">The height of the window. If this differs from <paramref name="renderHeight"/>,
    /// the video output will be scaled.</param>
    /// <param name="refreshRate">The frequency in which the video update routines should be called, in hertz.</param>
    /// <param name="title">The title of the window.</param>
    /// <param name="fullscreen">If this is <c>true</c>, the video output will be fullscreen. If this is <c>false</c>,
    /// it will be windowed. Even if this is set to <c>true</c>, make sure that sensible values are provided for
    /// <paramref name="windowWidth"/> and <paramref name="windowHeight"/> in case the window manager is unable to
    /// create a fullscreen surface.</param>
    /// <param name="textureFiltering">Sets the texture filtering method that will be used by this window's rendering
    /// surface. Note that this is only noticeable if the rendering surface is a different size from the window. This
    /// defaults to <see cref="TextureFiltering.Nearest"/>.</param>
    protected Window(int renderWidth, int renderHeight, int windowWidth, int windowHeight, int refreshRate, string title = "SDLKit Window", bool fullscreen = false, TextureFiltering textureFiltering = TextureFiltering.Nearest) {
        _fullscreen = fullscreen;
        _renderWidth = renderWidth;
        _renderHeight = renderHeight;
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
        _windowTitle = title;
        RenderTargetTextureFiltering = textureFiltering;

        CreateWindow();
        RefreshRate = refreshRate;
        
        if(_renderTarget is null) {
            return;
        }
        _renderTarget.Destination = new SdlRect(0, 0, _windowWidth, _windowHeight);
    }
    
    /// <summary>
    /// Returns the last error returned by SDL. This should only be called if the method's documentation explicitly
    /// calls for it, as otherwise the results may not make much sense.
    /// </summary>
    /// <returns>A string representing the last error the SDL library encountered.</returns>
    public string GetLastSdlError() => Error.GetError();

    /// <summary>
    /// <para>
    /// This should be called when the application is exiting. This will fully shut down SDL and any of its supporting
    /// libraries.
    /// </para>
    /// <para>
    /// It is good practice to dispose of any additional resources that you have allocated (such as
    /// <see cref="IRenderable"/> instances) prior to calling this method.
    /// </para>
    /// </summary>
    public void Quit() {
        if(Image.SdlImageInitialized) {
            // Shut down SDL_image.
            Interop.SdlImage.Image.Quit();
        }
        
        _renderTarget?.Dispose();
        _windowTarget?.Dispose();

        Render.DestroyRenderer(SdlRenderer);
        Video.DestroyWindow(_sdlWindow);

        if(Gamepad.Enabled) {
            Gamepad.DisableSubsystem();
        }
        
        Init.Quit();
    }

    /// <summary>
    /// Starts the application loop. This should be called after the application's initialization has been performed
    /// and control should be passed to the game loop.
    /// </summary>
    public void Start() {
        Video.ShowWindow(_sdlWindow);
        StartTimerLoop();
        Video.HideWindow(_sdlWindow);
    }

    /// <summary>
    /// Stops the application loop. This should be called after the application has released its own resources and is
    /// ready for the SDL to exit.
    /// </summary>
    public void Stop() {
        TimerRunning = false;
    }
}
