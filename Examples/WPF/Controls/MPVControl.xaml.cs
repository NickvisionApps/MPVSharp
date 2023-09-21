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
    
    public void LoadFromYtdlp(string url)
    {
        _client.SetProperty("ytdl", true);
        _client.LoadFile("https://www.youtube.com/watch?v=u9lj-c29dxI");
    }

    public void CyclePause() => _client.CyclePause();
}