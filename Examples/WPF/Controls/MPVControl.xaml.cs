using System.IO;
using System.Windows.Controls;

namespace Nickvision.MPVSharp.Examples.WPF.Controls;

public partial class MPVControl : UserControl
{
    private Client _client;
    private MPVHwndHost _hwndHost;
    
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
    
    public void Load(string url)
    {
        _client.LoadFile(url);
        _client.SetProperty("pause", false);
    }
    
    public void CyclePause() => _client.CyclePause();
}