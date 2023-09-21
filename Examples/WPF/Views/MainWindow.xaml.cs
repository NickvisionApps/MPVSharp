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

    private void OnExit(object sender, RoutedEventArgs e) => Close();

    private void OnLoadVideo(object sender, RoutedEventArgs e) => MPV.LoadFromYtdlp(TxtUrl.Text);

    private void OnPause(object sender, RoutedEventArgs e) => MPV.CyclePause();

    private void OnAbout(object sender, RoutedEventArgs e) => MessageBox.Show("WPF MPV# Example", "About", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
}
