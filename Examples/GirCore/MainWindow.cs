using Nickvision.MPVSharp;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Examples.GirCore;

/// <summary>
/// Main application window
/// </summary>
public partial class MainWindow : Gtk.ApplicationWindow
{
    private delegate bool GSourceFunc(nint data);
    
    [LibraryImport("libglib-2.0.so")]
    private static partial uint g_idle_add(GSourceFunc function, nint data);
    
    private readonly Gtk.GLArea _glArea;
    private readonly GSourceFunc _queueRender;
    private readonly Client _player;
    private RenderContext? _ctx;
    
    /// <summary>
    /// Construct window
    /// </summary>
    public MainWindow(Gtk.Application application)
    {
        SetSizeRequest(640, 400);
        var header = Gtk.HeaderBar.New();
        SetTitlebar(header);
        var title = Gtk.Label.New("Loading...");
        header.SetTitleWidget(title);
        var pauseButton = Gtk.Button.New();
        header.PackStart(pauseButton);
        _glArea = Gtk.GLArea.New();
        SetChild(_glArea);
        _glArea.SetVexpand(true);
        _glArea.SetHexpand(true);
        // Create MPV client
        _player = new Client();
        _player.SetProperty("ytdl", true);
        _player.RequestLogMessages("debug");
        _player.LogMessageReceived += (sender, e) =>
        {
            Console.Write($"[{e.Prefix}] {e.Text}"); // Log messages (e.Text) end with newline char
        };
        _player.PropertyChanged += (sender, e) =>
        {
            if (e.Name == "pause")
            {
                pauseButton.SetIconName((bool)e.Node! ? "media-playback-start-symbolic" : "media-playback-pause-symbolic");
            }
            if (e.Name == "media-title")
            {
                title.SetLabel((string)e.Node!);
            }
        };
        _player.ObserveProperty("pause");
        _player.ObserveProperty("media-title");
        pauseButton.OnClicked += (sender, e) => _player.CyclePause();
        // Setup rendering
        _queueRender = (x) =>
        {
            _glArea.QueueDraw();
            return false;
        };
        _glArea.OnRealize += OnRealizeGLArea;
        _glArea.OnUnrealize += OnUnrealizeGLArea;
        _glArea.OnRender += OnRenderGLArea;
        // Keyboard shortcuts
        var actSeekLeft = Gio.SimpleAction.New("seek-left", null);
        actSeekLeft.OnActivate += (sender, e) => _player.Seek(-1);
        AddAction(actSeekLeft);
        application.SetAccelsForAction("win.seek-left", new []{"a"});
        var actSeekRight = Gio.SimpleAction.New("seek-right", null);
        actSeekRight.OnActivate += (sender, e) => _player.Seek(1);
        AddAction(actSeekRight);
        application.SetAccelsForAction("win.seek-right", new []{"d"});
    }

    /// <summary>
    /// Occurs on GLArea initialization. Set up render context and start playing.
    /// </summary>
    /// <param name="sender">Gtk.Widget that invoked the event</param>
    /// <param name="e">EventArgs</param>
    private void OnRealizeGLArea(Gtk.Widget sender, EventArgs e)
    {
        _glArea.MakeCurrent();
        _player.Initialize();
        _ctx = _player.CreateRenderContext();
        // We create callback for MPV to call every time the screen needs to be redrawn,
        // the callback calls GtkGLArea.QueueDraw which in turn makes RenderContext draw image
        _ctx.SetupGL((x) => g_idle_add(_queueRender, IntPtr.Zero)); // QueueDraw must be called from GTK thread
        _player.Command("loadfile https://www.youtube.com/watch?v=UXqq0ZvbOnk append-play");
    }

    /// <summary>
    /// Occurs on GLArea destruction. Free MPV objects
    /// </summary>
    /// <param name="sender">Gtk.Widget that invoked the event</param>
    /// <param name="e">EventArgs</param>
    private void OnUnrealizeGLArea(Gtk.Widget sender, EventArgs e)
    {
        // Free RenderContext first, then the Client
        _ctx?.Dispose();
        _player.Dispose();
    }

    /// <summary>
    /// Occurs on GLArea redraw. Call RenderContext to redraw image
    /// </summary>
    /// <param name="sender">Gtk.Widget that invoked the event</param>
    /// <param name="e">EventArgs</param>
    private bool OnRenderGLArea(Gtk.Widget sender, EventArgs e)
    {
        _ctx?.RenderGL(_glArea.GetAllocatedWidth(), _glArea.GetAllocatedHeight());
        return false;
    }
}