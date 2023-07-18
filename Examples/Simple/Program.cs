using Nickvision.MPVSharp.Exmaples.Simple;

public class Program
{
    public static int Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine($"Usage: {AppDomain.CurrentDomain.FriendlyName} file1 file2...");
            return 1;
        }
        // Create and init player
        using var player = new Client();
        player.SetProperty("input-default-bindings", true);
        player.SetProperty("input-vo-keyboard", true);
        player.SetProperty("ytdl", true);
        player.SetProperty("osc", false);
        player.SetProperty("osd-msg1", "Position: ${time-pos}");
        player.Initialize();
        // Add paths to playlist and play
        foreach (var path in args)
        {
            Console.WriteLine($"Adding {path}...");
            player.LoadFile(path, "append");
        }
        player.Command("playlist-play-index 0");
        var alive = true;
        // Watch properties
        player.PropertyChanged += (sender, e) =>
        {
            Console.WriteLine($"{e.Name}: {e.Node.ToString()}");
        };
        player.ObserveProperty("playlist-pos");
        player.ObserveProperty("filename/no-ext");
        // Enable logging
        player.RequestLogMessages("info");
        player.LogMessageReceived += (sender, e) =>
        {
            Console.Write($"[{e.Prefix}] {e.Text}"); // Log messages (e.Text) end with newline char
        };
        // Application loop
        player.Destroyed += () => alive = false;
        while (alive)
        {
            Thread.Sleep(1000);
        }
        return 0;
    }
}