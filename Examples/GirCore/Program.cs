namespace Nickvision.MPVSharp.Examples.GirCore;

public class Program
{
    public static void Main(string[] args)
    {
        var application = Gtk.Application.New("org.nickvision.mpvsharp.example", Gio.ApplicationFlags.FlagsNone);
        var window = new Nickvision.MPVSharp.Examples.GirCore.MainWindow(application);
        application.OnActivate += (sender, e) =>
        {
            application.AddWindow(window);
            window.Present();
        };
        application.Run();
    }
}