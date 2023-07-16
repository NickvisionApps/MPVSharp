![Nuget](https://img.shields.io/nuget/v/Nickvision.MPVSharp)

# Nickvision.MPVSharp

<img width='128' height='128' alt='Logo' src='Nickvision.MPVSharp/Resources/logo-r.svg'/>

 **Use MPV in your C# apps**

MPVSharp provides C# bindings for [libmpv](https://mpv.io/manual/master/#embedding-into-other-programs-libmpv), a library for embedding amazing [MPV](https://mpv.io/) player into another apps.

# Installation
<a href='https://www.nuget.org/packages/Nickvision.MPVSharp/'><img width='140' alt='Download on Nuget' src='https://www.nuget.org/Content/gallery/img/logo-header.svg'/></a>

You also need `libmpv` installed on your system.

> **The package only supports Linux at the moment!**

# How to use

There are 2 namespaces:
* **`Nickvision.MPVSharp.Internal`** provides objects and methods that closely follow libmpv C API. You can find functions from `client.h` in `MPVClient.cs` and functions from `render.h` in `MPVRenderContext.cs`
* **`Nickvision.MPVSharp`** gives you another objects that work on top of `Internal` counterpart, but provide you with various helpers and convinience functions. In short, they are easier to use.

There are comments in the code, but they are just an adaptation of some parts of comments from libmpc, which is very well documented:
* [client.h](https://github.com/mpv-player/mpv/blob/release/0.35/libmpv/client.h)
* [render.h](https://github.com/mpv-player/mpv/blob/release/0.35/libmpv/render.h)
* [renger_gl.h](https://github.com/mpv-player/mpv/blob/release/0.35/libmpv/render_gl.h)

![GirCore Example](Examples/GirCore/Screenshot.png)

<sub>GirCore example playing [Charge](https://www.youtube.com/watch?v=UXqq0ZvbOnk) - Blender open movie, licensed under CC BY 4.0</sub>

If you're not familiar with libmpv at all, start by looking at MPVSharp examples, they are very simple but will give you basic knowledge how to start/embed MPV in your application.
* [Simple](Examples/Simple) - basic program playing your file/link. If the file is video, a window will be created by MPV itself.
* [GirCore](Examples/GirCore) - GTK4 application playing a video from youtube. Made with [GirCore](https://github.com/gircore/gir.core). Requires gtk-4 to be installed.
* [OpenGL](Examples/OpenGL) - [OpenTK](https://opentk.net/index.html) application playing a video from youtube.

# Chat
<a href='https://matrix.to/#/#nickvision:matrix.org'><img width='140' alt='Join our room' src='https://user-images.githubusercontent.com/17648453/196094077-c896527d-af6d-4b43-a5d8-e34a00ffd8f6.png'/></a>

# Dependencies
- [.NET 7](https://dotnet.microsoft.com/en-us/)

# Code of Conduct

This project follows the [GNOME Code of Conduct](https://wiki.gnome.org/Foundation/CodeOfConduct).
