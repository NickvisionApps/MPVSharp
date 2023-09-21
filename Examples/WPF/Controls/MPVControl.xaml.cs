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
        _client.SetProperty("keep-open", "yes"); // to not terminate on playlist end
        AddChild(_hwndHost);
    }
    
    public void Load(string url)
    {
        _client.SetProperty("ytdl", File.Exists(url));
        _client.LoadFile(url);
    }

    public void CyclePause() => _client.CyclePause();
}