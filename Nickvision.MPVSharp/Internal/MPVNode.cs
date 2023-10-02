using System;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Generic data storage
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public partial struct MPVNode
{
    [LibraryImport("mpv")]
    private static partial void mpv_free_node_contents(ref MPVNode node);

    [FieldOffset(0)]
    private readonly nint _string;
    [FieldOffset(0)]
    private readonly int _flag;
    [FieldOffset(0)]
    private readonly long _int64;
    [FieldOffset(0)]
    private readonly double _double;
    [FieldOffset(0)]
    private readonly nint _nodeList;
    [FieldOffset(0)]
    private readonly nint _byteArray;
    [FieldOffset(8)]
    public MPVFormat Format;

    /// <summary>
    /// Empty MPVNode
    /// </summary>
    public static MPVNode Empty { get; } = new MPVNode();

    /// <summary>
    /// Constructs MPVNode containing string
    /// </summary>
    /// <param name="str">String</param>
    /// <param name="format">Node format (String or OSDString)</param>
    /// <exception cref="ArgumentException">Thrown if format is not String or OSDString</exception>
    public MPVNode(string str, MPVFormat format = MPVFormat.String)
    {
        if (format != MPVFormat.String && format != MPVFormat.OSDString)
        {
            throw new ArgumentException("MPVNode with string should have format String or OSDString");
        }
        _string = Marshal.StringToCoTaskMemUTF8(str);
        Format = format;
    }

    /// <summary>
    /// Constructs MPVNode containing flag
    /// </summary>
    /// <param name="flag">Flag (0 or 1)</param>
    public MPVNode(int flag)
    {
        _flag = flag;
        Format = MPVFormat.Flag;
    }

    /// <summary>
    /// Constructs MPVNode containing Int64
    /// </summary>
    /// <param name="int64">Long int</param>
    public MPVNode(long int64)
    {
        _int64 = int64;
        Format = MPVFormat.Int64;
    }

    /// <summary>
    /// Constructs MPVNode containing Double
    /// </summary>
    /// <param name="dbl">Double</param>
    public MPVNode(double dbl)
    {
        _double = dbl;
        Format = MPVFormat.Double;
    }

    /// <summary>
    /// Constructs MPVNode containing <see cref="MPVNodeList"/>
    /// </summary>
    /// <param name="list">MPVNodeList</param>
    /// <param name="format">Node format (<see cref="MPVFormat.NodeMap"/> or <see cref="MPVFormat.NodeArray"/>)</param>
    /// <exception cref="ArgumentException">Thrown if format is not NodeMap or NodeArray</exception>
    public MPVNode(MPVNodeList list, MPVFormat format)
    {
        if (format != MPVFormat.NodeMap && format != MPVFormat.NodeArray)
        {
            throw new ArgumentException("MPVNode with MPVNodeList should have format NodeMap or NodeArray");
        }
        Marshal.StructureToPtr(list, _nodeList, true);
        Format = format;
    }

    /// <summary>
    /// Constructs MPVNode containing <see cref="MPVByteArray"/>
    /// </summary>
    /// <param name="ba">MPVByteArray</param>
    public MPVNode(MPVByteArray ba)
    {
        Marshal.StructureToPtr(ba, _byteArray, true);
        Format = MPVFormat.ByteArray;
    }

    /// <summary>
    /// Gets MPVNode from pointer
    /// </summary>
    /// <param name="data">Pointer to node</param>
    /// <returns>MPVNode from pointer</returns>
    public static MPVNode FromIntPtr(nint data) => Marshal.PtrToStructure<MPVNode>(data);

    /// <summary>
    /// Frees any data referenced by the node. It doesn't free the node itself.
    /// </summary>
    public static void FreeNodeContents(MPVNode node) => mpv_free_node_contents(ref node);
    
    public static explicit operator string?(MPVNode n) => Marshal.PtrToStringUTF8(n._string);

    public static explicit operator bool?(MPVNode n) => n.Format == MPVFormat.Flag ? n._flag == 1 : null;

    public static explicit operator long?(MPVNode n) => n.Format == MPVFormat.Int64 ? n._int64 : null;

    public static explicit operator double?(MPVNode n) => n.Format == MPVFormat.Double ? n._double : null;

    public static explicit operator MPVNode[]?(MPVNode n) => n.Format == MPVFormat.NodeArray ? (MPVNode[]?)Marshal.PtrToStructure<MPVNodeList>(n._nodeList) : null;

    public static explicit operator MPVNodeList?(MPVNode n) => n.Format == MPVFormat.NodeArray || n.Format == MPVFormat.NodeMap ? Marshal.PtrToStructure<MPVNodeList>(n._nodeList) : null;

    public static explicit operator MPVByteArray?(MPVNode n) => n.Format == MPVFormat.ByteArray ? Marshal.PtrToStructure<MPVByteArray>(n._byteArray) : null;

    /// <summary>
    /// Gets string representation of the node content.
    /// Not to be confused with string? cast.
    /// </summary>
    public override string ToString()
    {
        return Format switch
        {
            MPVFormat.Flag => _flag == 1 ? "True" : "False",
            MPVFormat.Int64 => _int64.ToString(),
            MPVFormat.Double => _double.ToString(),
            MPVFormat.NodeArray => Marshal.PtrToStructure<MPVNodeList>(_nodeList).ToString(),
            MPVFormat.NodeMap => Marshal.PtrToStructure<MPVNodeList>(_nodeList).ToString(),
            MPVFormat.ByteArray => _byteArray.ToString(),
            _ => Marshal.PtrToStringUTF8(_string)
        } ?? "";
    }
}