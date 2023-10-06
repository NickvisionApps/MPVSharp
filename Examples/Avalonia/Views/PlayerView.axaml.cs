using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Nickvision.MPVSharp;

namespace AvaloniaMPV.Views;

/// <summary>
/// Player view
/// </summary>
public partial class PlayerView : UserControl
{
    private readonly Client _player;
    
    /// <summary>
    /// Constructs PlayerView
    /// </summary>
    public PlayerView()
    {
        InitializeComponent();
        _player = MPVControl.Player;
        _player.LoadFile("https://www.youtube.com/watch?v=_cMxraX_5RE");
        _player.PropertyChanged += (sender, e) => Dispatcher.UIThread.Invoke(() => OnMPVPropertyChanged(e)); // Use Invoke, not Post, to avoid threading issues
        _player.ObserveProperty("pause");
        _player.ObserveProperty("time-pos");
    }
    
    /// <summary>
    /// Occurs when an MPV property has changed
    /// </summary>
    /// <param name="e">PropertyChangedEventArgs</param>
    public void OnMPVPropertyChanged(PropertyChangedEventArgs e)
    {
        switch (e.Name)
        {
            case "pause":
                PauseButton.Content = (bool)e.Node ? "Paused" : "Playing";
                break;
            case "time-pos":
                TimeLabel.Text = TimeSpan.FromSeconds((double)e.Node).ToString(@"mm\:ss");
                break;
        }
    }
    
    /// <summary>
    /// Occurs when pause button was clicked
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">RoutedEventArgs</param>
    public void OnPauseClicked(object sender, RoutedEventArgs e)
    {
        _player.CyclePause();
    }
}