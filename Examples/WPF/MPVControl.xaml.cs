using System.Windows;
using Nickvision.MPVSharp;
using System.Windows.Controls;

namespace Nickvision.MPVSharp.Examples.WPF;

public partial class MPVControl : UserControl
{
    public Client Client { get; init; }
    
    public MPVControl()
    {
        InitializeComponent();
        Client = new Client();
        Client.SetProperty("keep-open", "yes"); // to not terminate on playlist end
        Loaded += OnLoaded;
    }
    
    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
        var clientHost = new MPVHwndHost(Client);
        AddChild(clientHost);
    }
}