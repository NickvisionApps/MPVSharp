using Nickvision.MPVSharp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Nickvision.MPVSharp.Examples.OpenGL;

/// <summary>
/// Player window
/// </summary>
public class MPVWindow : GameWindow
{
    private readonly Client _player;
    private RenderContext? _ctx;
    
    /// <summary>
    /// Construct window
    /// </summary>
    /// <param name="width">Window width</param>
    /// <param name="height">Window height</param>
    /// <param name="title">Window title</param>
    public MPVWindow(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
    {
        // Create MPV Client
        _player = new Client();
        _player.SetProperty("ytdl", true);
        _player.RequestLogMessages("debug");
        _player.LogMessageReceived += (sender, e) =>
        {
            Console.Write($"[{e.Prefix}] {e.Text}"); // Log messages (e.Text) end with newline char
        };
    }
    
    /// <summary>
    /// Occurs when windows finishes loading
    /// </summary>
    protected override void OnLoad()
    {
        base.OnLoad();
        _player.Initialize();
        _ctx = _player.CreateRenderContext();
        // GameWindow calls OnRenderFrame, well, every frame, so we don't need callback for MPV to tell us when to draw
        _ctx.SetupGL(null);
        _player.LoadFile("https://www.youtube.com/watch?v=WhWc3b3KhnY");
    }
    
    /// <summary>
    /// Occurs every frame
    /// </summary>
    /// <param name="e">FrameEventArgs</param>
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        _ctx?.RenderGL(ClientSize.X, ClientSize.Y);
        if (KeyboardState.IsKeyReleased(Keys.Space))
        {
            _player.CyclePause();
        }
        if (KeyboardState.IsKeyDown(Keys.Left))
        {
            _player.Seek(-0.5);
        }
        if (KeyboardState.IsKeyDown(Keys.Right))
        {
            _player.Seek(0.5);
        }
        SwapBuffers();
    }
}