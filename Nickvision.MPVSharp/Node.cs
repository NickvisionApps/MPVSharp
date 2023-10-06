using System;
using Nickvision.MPVSharp.Internal;

namespace Nickvision.MPVSharp;

/// <summary>
/// Non-nullable wrapper around <see cref="MPVNode"/>
/// </summary>
public class Node
{
    private readonly MPVNode? _data;
    
    /// <summary>
    /// Wrapped <see cref="MPVNode"/> or <see cref="MPVNode.Empty">empty MPVNode</see> if wrapped object is null
    /// </summary>
    public MPVNode Data => _data ?? MPVNode.Empty;
    
    /// <summary>
    /// Format of the wrapped <see cref="MPVNode"/> or null
    /// </summary>
    public MPVFormat? Format => _data?.Format;
    
    /// <summary>
    /// Constructs Node from <see cref="MPVNode"/>
    /// </summary>
    /// <param name="data">MPVNode or null</param>
    public Node(MPVNode? data)
    {
        _data = data;
    }

    /// <summary>
    /// Constructs Node containing string
    /// </summary>
    /// <param name="str">String</param>
    /// <param name="format">Node format (String or OSDString)</param>
    public Node(string str, MPVFormat format = MPVFormat.String)
    {
        _data = new MPVNode(str, format);
    }
    
    /// <summary>
    /// Constructs Node containing Int64
    /// </summary>
    /// <param name="int64">Long int</param>
    public Node(long int64)
    {
        _data = new MPVNode(int64);
    }

    /// <summary>
    /// Constructs Node containing Double
    /// </summary>
    /// <param name="dbl">Double</param>
    public Node(double dbl)
    {
        _data = new MPVNode(dbl);
    }

    /// <summary>
    /// Constructs Node containing <see cref="MPVNodeList"/>
    /// </summary>
    /// <param name="list">NodeList</param>
    /// <param name="format">Node format (NodeMap or NodeArray)</param>
    public Node(MPVNodeList list, MPVFormat format)
    {
        _data = new MPVNode(list, format);
    }

    /// <summary>
    /// Constructs Node containing <see cref="MPVByteArray"/>
    /// </summary>
    /// <param name="ba">MPVByteArray</param>
    public Node(MPVByteArray ba)
    {
        _data = new MPVNode(ba);
    }
    
    public static explicit operator string(Node n) => (string?)n._data ?? "";

    public static explicit operator bool(Node n) => (bool?)n._data ?? false;

    public static explicit operator long(Node n) => (long?)n._data ?? Int64.MinValue;

    public static explicit operator double(Node n) => (double?)n._data ?? 0.0;

    public static explicit operator MPVNode[](Node n) => (MPVNode[]?)n._data ?? Array.Empty<MPVNode>();

    public static explicit operator MPVNodeList(Node n) => (MPVNodeList?)n._data ?? MPVNodeList.Empty;

    public static explicit operator MPVByteArray(Node n) => (MPVByteArray?)n._data ?? MPVByteArray.Empty;
}