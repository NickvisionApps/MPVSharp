using Microsoft.Win32;
using System;
using System.Windows;

namespace Nickvision.MPVSharp.Examples.WPF.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Constructs MainWindow
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Occurs when Open Video File button was clicked
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">RoutedEventArgs</param>
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

    /// <summary>
    /// Occurs when Exit action was activated
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">RoutedEventArgs</param>
    private void OnExit(object sender, RoutedEventArgs e) => Close();

    /// <summary>
    /// Occurs when Load Video button was clicked
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">RoutedEventArgs</param>
    private void OnLoadVideo(object sender, RoutedEventArgs e) => MPV.Load(TxtUrl.Text);

    /// <summary>
    /// Occurs when Pause button aws clicked
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">RoutedEventArgs</param>
    private void OnPause(object sender, RoutedEventArgs e) => MPV.CyclePause();

    /// <summary>
    /// Occurs when About action was activated
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">RoutedEventArgs</param>
    private void OnAbout(object sender, RoutedEventArgs e) => MessageBox.Show("WPF MPV# Example", "About", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
}
