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
    private readonly nint _values;
    /// <summary>
    /// Pointer to keys (IntPtr.Zero for NodeArray format)
    /// </summary>
    private readonly nint _keys;

    public static explicit operator MPVNode[]?(MPVNodeList n)
    {
        var result = new MPVNode[n.Num];
        for (var i = 0; i < n.Num; i++)
        {
            result[i] = Marshal.PtrToStructure<MPVNode>(n._values + i * 12);
        }
        return result;
    }
}