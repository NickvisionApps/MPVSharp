using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp;

/// <summary>
/// MPV event property struct
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVEventProperty
{
    /// <summary>
    /// Property name
    /// </summary>
    public string Name;
    /// <summary>
    /// Property MPV format
    /// </summary>
    public MPVFormat Format;
    /// <summary>
    /// Property value
    /// </summary>
    public nint Data;
}