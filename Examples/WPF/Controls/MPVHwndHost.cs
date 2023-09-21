using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Nickvision.MPVSharp.Examples.WPF.Controls;

/// <summary>
/// MPV Client Window Host
/// </summary>
/// <remarks>Code from https://github.com/milleniumbug/Mpv.WPF/blob/master/src/Mpv.WPF/MpvPlayerHwndHost.cs</remarks>
internal class MPVHwndHost : HwndHost
{
    [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
    internal static extern IntPtr CreateWindowEx(int dwExStyle,
        string lpClassName,
        string lpWindowName,
        int dwStyle,
        int x, int y,
        int nWidth, int nHeight,
        IntPtr hWndParent,
        IntPtr hMenu,
        IntPtr hInstance,
        IntPtr lpParam);
    [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
    internal static extern bool DestroyWindow(IntPtr hwnd);

    private const int WS_EX_TRANSPARENT = 0x00000020;
    private const int WS_CHILD = 0x40000000;
    private const int WS_VISIBLE = 0x10000000;
    private const int HOST_ID = 0x00000002;

    private readonly Client _client;
    
    /// <summary>
    /// Constructs MPVHwndHost
    /// </summary>
    /// <param name="client">MPV Client</param>
    public MPVHwndHost(Client client)
    {
        _client = client;
    }
    
    /// <summary>
    /// Creates the window to be hosted
    /// </summary>
    /// <param name="hwndParent">Parent window handle</param>
    /// <returns>The handle of created window</returns>
    protected override HandleRef BuildWindowCore(HandleRef hwndParent)
    {
        var playerHostPtr = CreateWindowEx(WS_EX_TRANSPARENT, "static", "", WS_CHILD | WS_VISIBLE,
            0, 0, 100, 100, hwndParent.Handle, HOST_ID, IntPtr.Zero, IntPtr.Zero);
        _client.SetProperty("wid", playerHostPtr.ToInt64());
        return new HandleRef(this, playerHostPtr);
    }

    /// <summary>
    /// Destroys the hosted window
    /// </summary>
    /// <param name="hwnd">The window handle</param>
    protected override void DestroyWindowCore(HandleRef hwnd)
    {
        DestroyWindow(hwnd.Handle);
    }
}