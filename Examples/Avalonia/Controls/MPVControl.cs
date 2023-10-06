using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using Nickvision.MPVSharp;
using static Avalonia.OpenGL.GlConsts;

namespace AvaloniaMPV.Controls;

/// <summary>
/// MPV Avalonia Control.
/// This is based on <see cref="Avalonia.OpenGL.Controls.OpenGlControlBase"/>.
/// It creates the <see cref="Player"/> and initializes it, then <see cref="RenderContext"/> is created
/// on OpenGL initialization and it gets configured to request new frame to be drawn when MPV requires it.
/// </summary>
public class MPVControl : OpenGlControlBase
{
    private RenderContext? _ctx;
    
    public Client Player { get; init; }
    
    /// <summary>
    /// Constructs MPVControl
    /// </summary>
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

    /// <summary>
    /// Occurs on OpenGL initialization
    /// </summary>
    /// <param name="gl">OpenGL interface</param>
    protected override void OnOpenGlInit(GlInterface gl)
    {
        if (_ctx != null)
        {
            // OnOpenGlInit is called twice, should be fixed in next Avalonia release
            // https://github.com/AvaloniaUI/Avalonia/pull/12713
            return;
        }
        _ctx = Player.CreateRenderContext();
        _ctx.GetProcAddressFn = gl.GetProcAddress; // Required to make it work on Windows
        _ctx.SetupGL(() => Dispatcher.UIThread.Post(RequestNextFrameRendering)); // Use Post, not Invoke, to avoid lags
    }

    /// <summary>
    /// Occurs on OpenGL deiniitalization
    /// </summary>
    /// <param name="gl">OpenGL interface</param>
    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        _ctx?.Dispose();
        Player.Dispose();
    }

    /// <summary>
    /// Used to draw a frame
    /// </summary>
    /// <param name="gl">OpenGL interface</param>
    /// <param name="fb">Framebuffer object name</param>
    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        gl.ClearColor(0, 0, 0, 1);
        gl.Clear(GL_COLOR_BUFFER_BIT);
        _ctx?.RenderGL((int)Bounds.Width, (int)Bounds.Height, fb);
    }
}