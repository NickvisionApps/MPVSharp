using System;
using System.Windows;

namespace Nickvision.MPVSharp.Examples.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }
    
    private void OnLoaded(object sender, EventArgs e)
    {
        MPVControl.Client.Initialize();
        MPVControl.Client.SetProperty("ytdl", true);
        MPVControl.Client.LoadFile("https://www.youtube.com/watch?v=u9lj-c29dxI");
    }
    
    private void OnPauseClicked(object sender, EventArgs e) => MPVControl.Client.CyclePause();
}
