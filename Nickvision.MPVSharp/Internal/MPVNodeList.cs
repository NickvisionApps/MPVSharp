using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Node List
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVNodeList
{
    /// <summary>
    /// Number of elements
    /// </summary>
    public int Num;
    /// <summary>
    /// Pointer to values
    /// </summary>
    public nint Values;
    /// <summary>
    /// Pointer to keys (IntPtr.Zero for NodeArray format)
    /// </summary>
    public nint Keys;
}