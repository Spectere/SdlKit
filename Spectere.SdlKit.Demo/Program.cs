using Spectere.SdlKit.Demo;

// This just initializes the application, performs some basic sanity checks, and passes control to the window. Most
// of the actual logic is contained in other classes.


// To run this with a different scale, simply pass the desired scale as a command-line parameter. If an invalid value
// is specified, or none at all, default to 2x.

var scale = 2.0m;
if(args.Length > 0) {
    if(!decimal.TryParse(args[0], out scale)) {
        Console.WriteLine($"Invalid scale value: '{scale}'. Defaulting to {scale}x.");
    }
}

// Initialize the window and start the application/game loop.
var window = new AppWindow(scale);
window.Start();

// Tear down the window and all of its unmanaged resources.
window.Quit();
