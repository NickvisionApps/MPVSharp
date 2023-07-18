namespace Nickvision.MPVSharp.Examples.OpenGL;

public class Program
{
    public static void Main(string[] args)
    {
        using var window = new MPVWindow(640, 360, "OpenTK MPV Example");
        window.Run();
    }
}