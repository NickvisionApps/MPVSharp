using Microsoft.Win32;
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

    private void OnOpenVideo(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
            Filter = "Video files (*.mp4;*.mov;*.avi;*.wmv;*.flv)|*.mp4;*.mov;*.avi;*.wmv;*.flv"
        };
        if(openFileDialog.ShowDialog() == true)
        {
            TxtUrl.Text = openFileDialog.FileName;
        }
        OnLoadVideo(sender, e);
    }

    private void OnExit(object sender, RoutedEventArgs e) => Close();

    private void OnLoadVideo(object sender, RoutedEventArgs e) => MPV.Load(TxtUrl.Text);

    private void OnPause(object sender, RoutedEventArgs e) => MPV.CyclePause();

    private void OnAbout(object sender, RoutedEventArgs e) => MessageBox.Show("WPF MPV# Example", "About", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
}
