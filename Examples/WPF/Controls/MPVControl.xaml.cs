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
        _client.SetProperty("ytdl", true);
        AddChild(_hwndHost);
    }
    
    public void LoadFromYtdlp(string url) => _client.LoadFile(url);

    public void CyclePause() => _client.CyclePause();
}