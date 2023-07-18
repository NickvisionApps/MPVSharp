# MPVSharp API

* **`Nickvision.MPVSharp.Internal`** provides objects and methods that closely follow libmpv C API. You can find functions from `client.h` in `MPVClient.cs` and functions from `render.h` in `MPVRenderContext.cs`
* **`Nickvision.MPVSharp`** gives you another objects that work on top of `Internal` counterpart, but provides you with various helpers and convenience functions. They are easier to use, but the API is slightly different than in `Internal`, so you will probably want to read [`Client class`](Nickvision.MPVSharp.Client.html) to find what methods you can use.

Look at the [examples](https://github.com/NickvisionApps/MPVSharp/tree/main/Examples) to get the basic idea how to use MPVSharp.