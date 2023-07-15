var application = Gtk.Application.New("org.nickvision.mpvsharp.example", Gio.ApplicationFlags.FlagsNone);
var window = new Nickvision.MPVSharp.Example.GirCore.MainWindow();
application.OnActivate += (sender, e) =>
{
    application.AddWindow(window);
    window.Present();
};
application.Run();