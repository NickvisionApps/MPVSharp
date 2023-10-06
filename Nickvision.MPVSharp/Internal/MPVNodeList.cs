using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Array/Dictionary-like MPV structure
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVNodeList
{
    /// <summary>
    /// Number of entries
    /// </summary>
    public int Num;
    /// <summary>
    /// Pointer to values
    /// </summary>
    private readonly nint _values;
    /// <summary>
    /// Pointer to keys for NodeMap or IntPtr.Zero for NodeArray
    /// </summary>
    private readonly nint _keys;

    private const int NODE_SIZE = 16;

    /// <summary>
    /// Empty MPVNodeList
    /// </summary>
    public static MPVNodeList Empty { get; } = new MPVNodeList { Num = 0 };

    public static explicit operator MPVNode[](MPVNodeList n)
    {
        var result = new MPVNode[n.Num];
        for (var i = 0; i < n.Num; i++)
        {
            result[i] = Marshal.PtrToStructure<MPVNode>(n._values + i * NODE_SIZE);
        }
        return result;
    }

    public static explicit operator Dictionary<string, MPVNode>?(MPVNodeList n)
    {
        if (n._keys == IntPtr.Zero)
        {
            return null;
        }
        var arr = (MPVNode[])n;
        var result = new Dictionary<string, MPVNode> ();
        for (var i = 0; i < n.Num; i++)
        {
            var key = Marshal.PtrToStringUTF8(Marshal.ReadIntPtr(n._keys + i * nint.Size));
            result[key!] = arr[i];
        }
        return result;
    }

    public static Dictionary<string, MPVNode>? ToDictionary(MPVNodeList n) => (Dictionary<string, MPVNode>?)n;

    public override string ToString() => (_keys == IntPtr.Zero) ? "MPVNodeArray" : "MPVNodeMap";
}