using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV data node
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 12)]
public struct MPVNode {
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
    
    public static MPVNode FromIntPtr(nint data) => Marshal.PtrToStructure<MPVNode>(data);
    
    public static explicit operator string?(MPVNode n) => Marshal.PtrToStringUTF8(n._string);

    public static explicit operator bool?(MPVNode n) => n.Format == MPVFormat.Flag ? n._flag == 1 : null;

    public static explicit operator long?(MPVNode n) => n.Format == MPVFormat.Int64 ? n._int64 : null;

    public static explicit operator double?(MPVNode n) => n.Format == MPVFormat.Double ? n._double : null;

    public static explicit operator MPVNode[]?(MPVNode n) => n.Format == MPVFormat.NodeArray ? (MPVNode[]?)Marshal.PtrToStructure<MPVNodeList>(n._nodeList) : null;

    public static explicit operator MPVNodeList?(MPVNode n) => n.Format == MPVFormat.NodeArray || n.Format == MPVFormat.NodeMap ? Marshal.PtrToStructure<MPVNodeList>(n._nodeList) : null;

    public static explicit operator MPVByteArray?(MPVNode n) => n.Format == MPVFormat.ByteArray ? Marshal.PtrToStructure<MPVByteArray>(n._byteArray) : null;

    public override string ToString()
    {
        return Format switch
        {
            MPVFormat.Flag => _flag == 1 ? "True" : "False",
            MPVFormat.Int64 => _int64.ToString(),
            MPVFormat.Double => _double.ToString(),
            MPVFormat.NodeArray => $"{Marshal.PtrToStructure<MPVNodeList>(_nodeList).ToString()} (NodeArray)",
            MPVFormat.NodeMap => $"{Marshal.PtrToStructure<MPVNodeList>(_nodeList).ToString()} (NodeMap)",
            MPVFormat.ByteArray => _byteArray.ToString(),
            _ => Marshal.PtrToStringUTF8(_string)
        } ?? "";
    }
}