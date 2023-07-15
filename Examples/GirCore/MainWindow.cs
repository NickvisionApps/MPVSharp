using Nickvision.MPVSharp;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Example.GirCore;

public partial class MainWindow : Gtk.ApplicationWindow
{
    private delegate bool GSourceFunc(nint data);
    
    [LibraryImport("libadwaita-1.so.0")]
    private static partial uint g_idle_add(GSourceFunc function, nint data);
    
    private readonly Gtk.GLArea _glArea;
    private readonly GSourceFunc _queueRender;
    private readonly Client _player;
    private RenderContext? _ctx;
    private RenderContext.MPVRenderUpdateFn _renderCallback;
    
    public MainWindow()
    {
        _player = new Client();
        _player.SetProperty("ytdl", true);
        _player.RequestLogMessages("debug");
        _player.LogMessageReceived += (sender, e) =>
        {
            Console.Write($"[{e.Prefix}] {e.Text}"); // Log messages (e.Text) end with newline char
        };
        SetSizeRequest(640, 360);
        var header = Gtk.HeaderBar.New();
        SetTitlebar(header);
        _glArea = Gtk.GLArea.New();
        SetChild(_glArea);
        _glArea.SetVexpand(true);
        _glArea.SetHexpand(true);
        _queueRender = (x) =>
        {
            _glArea.QueueDraw();
            return false;
        };
        _renderCallback = RenderCallback;
        _glArea.OnRealize += (sender, e) =>
        {
            _glArea.MakeCurrent();
            _player.Initialize();
            _ctx = _player.CreateRenderContext();
            _ctx.SetupGL(_renderCallback);
            _player.Command("loadfile https://www.youtube.com/watch?v=qWVRJsaUTIg append-play");
        };
        _glArea.OnUnrealize += (sender, e) =>
        {
            _ctx.Dispose();
            _player.Dispose();
        };
        _glArea.OnRender += (sender, e) =>
        {
            _ctx.RenderGL(_glArea.GetAllocatedWidth(), _glArea.GetAllocatedHeight());
            return false;
        };
    }
    
    public void RenderCallback(nint data)
    {
        g_idle_add(_queueRender, IntPtr.Zero);
    }
}