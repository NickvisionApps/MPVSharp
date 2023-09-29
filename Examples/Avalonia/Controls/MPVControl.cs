using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using Nickvision.MPVSharp;
using static Avalonia.OpenGL.GlConsts;

namespace AvaloniaMPV.Controls;

public class MPVControl : OpenGlControlBase
{
    private RenderContext? _ctx;
    
    public readonly Client Player;
    
    public MPVControl()
    {
        Player = new Client();
        Player.SetProperty("ytdl", true);
        Player.RequestLogMessages("debug");
        Player.LogMessageReceived += (sender, e) =>
        {
            System.Console.Write($"[{e.Prefix}] {e.Text}"); // Log messages (e.Text) end with newline char
        };
        Player.Initialize();
    }

    protected override void OnOpenGlInit(GlInterface gl)
    {
        if (_ctx != null)
        {
            // OnOpenGlInit is called twice, should be fixed in next Avalonia release
            // https://github.com/AvaloniaUI/Avalonia/pull/12713
            return;
        }
        _ctx = Player.CreateRenderContext();
        _ctx.SetupGL(() => Dispatcher.UIThread.Post(RequestNextFrameRendering));
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        _ctx?.Dispose();
        Player.Dispose();
    }

    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        gl.ClearColor(0, 0, 0, 1);
        gl.Clear(GL_COLOR_BUFFER_BIT);
        _ctx?.RenderGL((int)Bounds.Width, (int)Bounds.Height);
    }
}