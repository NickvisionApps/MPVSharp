using System;
using System.Windows;

namespace Nickvision.MPVSharp.Examples.WPF.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e) => MPV.LoadFromYtdlp("https://www.youtube.com/watch?v=u9lj-c29dxI");

    private void OnPauseClicked(object sender, EventArgs e) => MPV.CyclePause();
}
