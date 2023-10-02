using Nickvision.MPVSharp;
using System;

namespace Nickvision.MPVSharp.Examples.GirCore;

/// <summary>
/// Main application window
/// </summary>
public partial class MainWindow : Gtk.ApplicationWindow
{
    private const uint GDK_KEY_Left = 0xff51;
    private const uint GDK_KEY_Right =  0xff53;
    private const uint GDK_KEY_space = 0x020;

    private readonly Gtk.GLArea _glArea;
    private readonly Client _player;
    private RenderContext? _ctx;
    private (int, int) _size;
    
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
        _size = (0, 0);
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
                pauseButton.SetIconName((bool)e.Node ? "media-playback-start-symbolic" : "media-playback-pause-symbolic");
            }
            if (e.Name == "media-title")
            {
                title.SetLabel((string)e.Node);
            }
        };
        _player.ObserveProperty("pause");
        _player.ObserveProperty("media-title");
        pauseButton.OnClicked += (sender, e) => _player.CyclePause();
        // Setup rendering
        _glArea.OnRealize += OnRealizeGLArea;
        _glArea.OnUnrealize += OnUnrealizeGLArea;
        _glArea.OnRender += OnRenderGLArea;
        _glArea.OnResize += (_, e) => _size = (e.Width, e.Height);
        // Keyboard shortcuts
        var keyboardController = Gtk.EventControllerKey.New();
        keyboardController.SetPropagationPhase(Gtk.PropagationPhase.Capture);
        keyboardController.OnKeyPressed += OnKeyPressed;
        keyboardController.OnKeyReleased += OnKeyReleased;
        AddController(keyboardController);
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
        _ctx.SetupGL(() => GLib.Functions.IdleAdd(0, () =>
        {
            _glArea.QueueDraw(); // QueueDraw must be called from GTK thread
            return false;
        }));
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
        _ctx?.RenderGL(_size.Item1, _size.Item2);
        return false;
    }

    /// <summary>
    /// Occurs when a key was pressed
    /// </summary>
    /// <param name="sender">Key controller</param>
    /// <param name="e">Event args</param>
    /// <returns>Whether the key press was handled or not</returns>
    private bool OnKeyPressed(Gtk.EventControllerKey sender, Gtk.EventControllerKey.KeyPressedSignalArgs e)
    {
        switch (e.Keyval)
        {
            case GDK_KEY_Left:
                _player.Seek(-1);
                break;
            case GDK_KEY_Right:
                _player.Seek(1);
                break;
        };
        return true;
    }

    /// <summary>
    /// Occurs when a key was released
    /// </summary>
    /// <param name="sender">Key controller</param>
    /// <param name="e">Event args</param>
    private void OnKeyReleased(Gtk.EventControllerKey sender, Gtk.EventControllerKey.KeyReleasedSignalArgs e)
    {
        if (e.Keyval == GDK_KEY_space)
        {
            _player.CyclePause();
        }
    }
}