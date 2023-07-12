using Nickvision.MPVSharp;
using System;
using System.Threading;

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
        var player = new MPVClient();
        player.SetProperty("input-default-bindings", true);
        player.SetProperty("input-vo-keyboard", true);
        player.SetProperty("ytdl", true);
        player.SetProperty("osc", true);
        player.SetProperty("osd-msg1", "Position: ${time-pos}");
        Console.WriteLine($"Init: {player.Initialize()}");
        // Add paths to playlist and play
        foreach (var path in args)
        {
            Console.WriteLine($"Adding {path}...");
            player.Command(new string[] {"loadfile", path, "append"});
        }
        player.Command("playlist-play-index 0");
        var alive = true;
        // Watch properties
        player.PropertyChanged += (sender, e) =>
        {
            Console.WriteLine($"{e.Name}: {e.Data}");
        };
        player.ObserveProperty("pause");
        player.ObserveProperty("playlist/count");
        player.ObserveProperty("filename/no-ext");
        // Application loop
        player.Destroyed += () => alive = false;
        while (alive)
        {
            Thread.Sleep(1000);
        }
        return 0;
    }
}