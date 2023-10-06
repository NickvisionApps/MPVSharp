using System.IO;
using System.Windows.Controls;

namespace Nickvision.MPVSharp.Examples.WPF.Controls;

/// <summary>
/// MPV WPF Control.
/// Embedded window is created and MPV window is connected to it (see <see cref="MPVHwndHost"/>).
/// </summary>
public partial class MPVControl : UserControl
{
    private Client _client;
    private MPVHwndHost _hwndHost;
    
    /// <summary>
    /// Constructs MPVControl
    /// </summary>
    public MPVControl()
    {
        InitializeComponent();
        _client = new Client();
        _hwndHost = new MPVHwndHost(_client);
        _client.Initialize();
        _client.SetProperty("keep-open", "yes"); // to pause on playlist end
        _client.SetProperty("ytdl", true); // to enable yt-dlp support
        AddChild(_hwndHost);
    }
    
    /// <summary>
    /// Loads and plays file from path or URL
    /// </summary>
    /// <param name="url">Path or URL</param>
    public void Load(string url)
    {
        _client.LoadFile(url);
        _client.SetProperty("pause", false);
    }
    
    /// <summary>
    /// Toggles pause
    /// </summary>
    public void CyclePause() => _client.CyclePause();
}