using Spectere.SdlKit.Exceptions;
using Spectere.SdlKit.Interop.Sdl;
using Spectere.SdlKit.Interop.Sdl.Support.Init;
using Spectere.SdlKit.Interop.Sdl.Support.Render;
using Spectere.SdlKit.Interop.Sdl.Support.Video;
using Spectere.SdlKit.Renderables;

namespace Spectere.SdlKit;

public partial class Window {
    /// <summary>
    /// The <see cref="SdlWindow"/> structure used internally by SDL.
    /// </summary>
    private SdlWindow _sdlWindow;
    
    /// <summary>
    /// A null rendering target that is used to represent the SDL window.
    /// </summary>
    private RenderTarget? _windowTarget;
    
    /// <summary>
    /// The rendering target that all <see cref="IRenderable"/> instances are composited to.
    /// </summary>
    private RenderTarget? _renderTarget;
    
    /// <summary>
    /// The <see cref="SdlRenderer"/> structure used internally by SDL.
    /// </summary>
    internal SdlRenderer SdlRenderer;
    
    /// <summary>
    /// A GUID identifying the video update timer.
    /// </summary>
    private Guid _videoTimerGuid = Guid.Empty;

    /// <summary>
    /// Gets or sets this window's rendering target's color modulation value. This will effectively tint the entire
    /// screen.
    /// </summary>
    public SdlColor ColorModulation {
        get => _renderTarget?.ColorModulation ?? new SdlColor();
        set {
            if(_renderTarget is null) {
                return;
            }
            
            _renderTarget.ColorModulation = value;
        }
    }

    /// <summary>
    /// Gets or sets whether or not this window appears in fullscreen.
    /// </summary>
    public bool Fullscreen {
        get => _fullscreen;
        set {
            if(_sdlWindow.IsNull) {
                return;
            }
            
            var windowFlags = value ? WindowFlags.FullscreenDesktop : WindowFlags.None;
            if(Video.SetWindowFullscreen(_sdlWindow, windowFlags) == 0) {
                _fullscreen = value;
            }
        }
    }
    private bool _fullscreen;
    
    /// <summary>
    /// Gets or sets the video refresh rate of this window, in hertz.
    /// </summary>
    public int RefreshRate {
        get => _refreshRate;
        set {
            _refreshRate = value;
            RegisterVideoTimer();
        }
    }
    private int _refreshRate;

    /// <summary>
    /// Gets the height of the rendering area.
    /// </summary>
    public int RenderHeight => _renderHeight;
    private int _renderHeight;

    /// <summary>
    /// Gets the texture filtering mode used by the render target.
    /// </summary>
    public TextureFiltering RenderTargetTextureFiltering { get; } 

    /// <summary>
    /// Gets the width of the rendering area.
    /// </summary>
    public int RenderWidth => _renderWidth;
    private int _renderWidth;
    
    /// <summary>
    /// Adds a renderable to the drawing list of this window's <see cref="RenderTarget"/>.
    /// </summary>
    /// <param name="renderable">The <see cref="Renderable"/> to add to the list.</param>
    public void AddRenderable(Renderable renderable) => _renderTarget?.AddRenderable(renderable);

    /// <summary>
    /// Composites and renders all of the associated <see cref="IRenderable"/> instances.
    /// </summary>
    private void CompositeAndRender() {
        _windowTarget?.Update();
        Render.RenderPresent(SdlRenderer);
    }
    
        /// <summary>
    /// Creates a window and its rendering target.
    /// </summary>
    /// <exception cref="SdlRendererInitializationException">Thrown when the SDL renderer fails to initialize.</exception>
    /// <exception cref="SdlWindowInitializationException">Thrown when the SDL window fails to initialize.</exception>
    private void CreateWindow() {
        // Initialize the appropriate subsystems.
        const SubsystemFlags subsystemFlags = SubsystemFlags.Events | SubsystemFlags.Video;
        var result = Init.InitSubSystem(subsystemFlags);
        if(result != 0) {
            var message = Error.GetError();
            throw new SdlInitializationException(subsystemFlags, message);
        }
        
        // Initialize the SDL window.
        _sdlWindow = Video.CreateWindow(
            _windowTitle, 
            Video.WindowPosCenteredDisplay(0), Video.WindowPosCenteredDisplay(0),
            _windowWidth, _windowHeight,
            _fullscreen ? WindowFlags.FullscreenDesktop : WindowFlags.None
        );
        
        if(_sdlWindow.IsNull) {
            var message = Error.GetError();
            throw new SdlWindowInitializationException(message);
        }

        // Initialize the SDL renderer.
        SdlRenderer = Render.CreateRenderer(
            _sdlWindow, -1,
            RendererFlags.Accelerated | RendererFlags.TargetTexture
        );

        if(SdlRenderer.IsNull) {
            var message = Error.GetError();
            throw new SdlRendererInitializationException(message);
        }
        
        // Set up a rendering target for this window, as well as the scaled rendering surface.
        _windowTarget = new RenderTarget(SdlRenderer);
        
        _renderTarget = new RenderTarget(SdlRenderer, _renderWidth, _renderHeight, RenderTargetTextureFiltering);
        _windowTarget.AddRenderable(_renderTarget);
    }

    /// <summary>
    /// Removes a renderable from the drawing list of this window's <see cref="RenderTarget"/>.
    /// </summary>
    /// <param name="renderable">The <see cref="Renderable"/> to add to the list.</param>
    public void DeleteRenderable(Renderable renderable) => _renderTarget?.DeleteRenderable(renderable);
    
    /// <summary>
    /// Destroys the window and its renderer. Please note that this will only dispose of these two objects. You
    /// should dispose of any additional resources that you have allocated before calling this, including any
    /// <see cref="IRenderable"/> instances.
    /// </summary>

    
    /// <summary>
    /// Registers the video update timer. If it has already been registered, this will remove the existing registration
    /// and reregister it.
    /// </summary>
    private void RegisterVideoTimer() {
        if(_videoTimerGuid != Guid.Empty) {
            DeleteTimer(_videoTimerGuid);
        }
        
        _videoTimerGuid = AddTimer(VideoTimer, Timer.HertzToNanoseconds(RefreshRate));
    }
    
    /// <summary>
    /// Resizes the rendering target associated with this window. Note that this only resizes the one that draws
    /// directly to the window services. Any another <see cref="IRenderable"/> in the rendering tree needs to match
    /// the size of this window's rendering target they will need to be resized independently.
    /// </summary>
    /// <param name="newRenderTargetWidth">The new width of the rendering target.</param>
    /// <param name="newRenderTargetHeight">The new height of the rendering target.</param>
    public void ResizeRenderTarget(int newRenderTargetWidth, int newRenderTargetHeight) {
        _renderWidth = newRenderTargetWidth;
        _renderHeight = newRenderTargetHeight;

        if(_renderTarget is null) {
            return;
        }
        _renderTarget.Resize(_renderWidth, _renderHeight);
        _renderTarget.Destination = new SdlRect(0, 0, _windowWidth, _windowHeight);
    }

    /// <summary>
    /// Resizes this window.
    /// </summary>
    /// <param name="newWindowWidth">The new window width.</param>
    /// <param name="newWindowHeight">The new window height.</param>
    public void ResizeWindow(int newWindowWidth, int newWindowHeight) {
        _windowWidth = newWindowWidth;
        _windowHeight = newWindowHeight;

        Video.SetWindowSize(_sdlWindow, newWindowHeight, newWindowHeight);
        
        if(_renderTarget is null) {
            return;
        }
        _renderTarget.Destination = new SdlRect(0, 0, _windowWidth, _windowHeight);
    }
    
    /// <summary>
    /// This method is called after the final image is composited and rendered. <i>Do not</i> use this method to
    /// handle gameplay or application logic. Those should be handled in a separate update method and added to the
    /// timer using <see cref="Timer.AddTimer"/>.
    /// </summary>
    protected virtual void VideoPostRender() { }
    
    /// <summary>
    /// This method is called prior to the final image being composited and rendered. This should perform the final
    /// steps involved with preparing the scene before it is drawn to the screen. <i>Do not</i> use this method to
    /// handle gameplay or application logic. Those should be handled in a separate update method and added to the
    /// timer using <see cref="Timer.AddTimer"/>.
    /// </summary>
    /// <param name="delta">The amount of time since the last rendering pass.</param>
    protected virtual void VideoPreRender(long delta) { }

    /// <summary>
    /// Called by the periodic video timer, at the rate defined by <see cref="RefreshRate"/>. This is responsible for
    /// signaling to the implementing application that the screen is ready to be rendered, as well as starting the
    /// rendering process.
    /// </summary>
    /// <param name="delta">The amount of time since the last rendering pass.</param>
    private void VideoTimer(long delta) {
        VideoPreRender(delta);
        CompositeAndRender();
        VideoPostRender();
    }
}
