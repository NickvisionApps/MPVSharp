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
        var player = new MPVClient();
        player.SetProperty("input-default-bindings", true);
        player.SetProperty("input-vo-keyboard", true);
        player.SetProperty("ytdl", true);
        player.SetProperty("osc", true);
        Console.WriteLine($"Init: {player.Initialize()}");
        foreach (var path in args)
        {
            Console.WriteLine($"Adding {path}...");
            player.Command(new string[] {"loadfile", path, "append"});
        }
        player.Command("playlist-play-index 0");
        var alive = true;
        player.Destroyed += () => alive = false;
        player.FlagPropertyChanged += (sender, e) =>
        {
            if (e.Name == "pause")
            {
                Console.WriteLine($"Paused: {e.Data == 1}");
            }
        };
        player.ObserveProperty("pause", MPVFormat.Flag);
        while (alive)
        {
            player.SetProperty("osd-msg1", $"Position: ${{time-pos}}");
            Thread.Sleep(1000);
        }
        return 0;
    }
}