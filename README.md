# SDLKit

A kinda high-ish level bridge between SDL2 and .NET.


## But why, though?

I like SDL and I like C#, but whenever I do a project where I mash the two of them together it feels like I always end
up having to spend a bunch of time writing interop definitions and setting up SDL. Rather than maintaining a template
or anything like that, I figured I'd write a library that I could easily pull down from NuGet whenever I need to plot
any fancy pixels or play beeps and/or boops.


## What does this provide?

### Window

* Plops a window on your desktop. Nothing special here!


### Video

* Pixel plotting
* Image loading (via SDL_image)
* Support for an arbitrary number of render targets


### Timer

* Callback-based


### Audio

* Callback-based
* Audio in SDLKit currently accepts floats and will convert it to whatever data type the OS demands
* Both big- and little-endian systems should be supported, if you happen to be a PPC user running modern .NET stuff :)


### Events

* Window/WM
* Keyboard
* Mouse
* Gamepad


### "Renderables"

Higher level surfaces that might come in handy.

* TextConsole: a console that allows you to load a file and easily `Write` text to (or plot characters onto it)
* TileGrid: a multi-layer graphical surface that lets you scatter tiles from a tile sheet around


## How do I do the thing?

Documentation is fairly sparse at the moment, as I'm sure there's still a lot of tweaking I'll have to do to make this
useful for my purposes, let alone a general audience. The demo project is going to be your best source of documentation
for the time being.

I've also taken a lot of time to write XMLDocs across the board, so simply poking around with your IDE should give you
quite a bit of information to work with.


## I have an idea!

Feel free to open an issue and we can discuss it.


## You did XYZ wrong!

Maybe! Feel free to open an issue to let me know, or show me how it's done by opening a pull request.
