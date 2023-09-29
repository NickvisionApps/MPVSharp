using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using AvaloniaMPV.Controls;
using Nickvision.MPVSharp;

namespace AvaloniaMPV.Views;

public partial class PlayerView : UserControl
{
    private readonly Client _player;
    
    public PlayerView()
    {
        InitializeComponent();
        _player = MPVControl.Player;
        _player.LoadFile("https://www.youtube.com/watch?v=_cMxraX_5RE");
        _player.PropertyChanged += (sender, e) => Dispatcher.UIThread.Invoke(() => OnMPVPropertyChanged(sender ,e));
        _player.ObserveProperty("pause");
        _player.ObserveProperty("time-pos");
    }
    
    public void OnMPVPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.Node == null)
        {
            return;
        }
        switch (e.Name)
        {
            case "pause":
                PauseButton.Content = (bool)e.Node! ? "Paused" : "Playing";
                break;
            case "time-pos":
                TimeLabel.Text = TimeSpan.FromSeconds((double)e.Node!).ToString(@"mm\:ss");
                break;
        }
    }
    
    public void OnPauseClicked(object sender, RoutedEventArgs e)
    {
        _player.CyclePause();
    }
}