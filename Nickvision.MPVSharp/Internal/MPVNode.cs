using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV data node
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct MPVNode {
    [FieldOffset(0)]
    public nint String;
    [FieldOffset(0)]
    public int Flag;
    [FieldOffset(0)]
    public long Int64;
    [FieldOffset(0)]
    public double Double;
    [FieldOffset(0)]
    public nint NodeList;
    [FieldOffset(0)]
    public nint ByteArray;
    [FieldOffset(8)]
    public MPVFormat Format;
    
    public static MPVNode FromIntPtr(nint data) => Marshal.PtrToStructure<MPVNode>(data);
    
    public static explicit operator string?(MPVNode n) => Marshal.PtrToStringUTF8(n.String);

    public static explicit operator bool?(MPVNode n) => n.Format == MPVFormat.Flag ? n.Flag == 1 : null;

    public static explicit operator long?(MPVNode n) => n.Format == MPVFormat.Int64 ? n.Int64 : null;

    public static explicit operator double?(MPVNode n) => n.Format == MPVFormat.Double ? n.Double : null;

    public static explicit operator MPVNodeList?(MPVNode n) => n.Format == MPVFormat.NodeArray || n.Format == MPVFormat.NodeMap ? Marshal.PtrToStructure<MPVNodeList>(n.NodeList) : null;

    public static explicit operator MPVByteArray?(MPVNode n) => n.Format == MPVFormat.ByteArray ? Marshal.PtrToStructure<MPVByteArray>(n.ByteArray) : null;

    public override string ToString()
    {
        return Format switch
        {
            MPVFormat.Flag => Flag == 1 ? "True" : "False",
            MPVFormat.Int64 => Int64.ToString(),
            MPVFormat.Double => Double.ToString(),
            MPVFormat.NodeArray => $"{Marshal.PtrToStructure<MPVNodeList>(NodeList).ToString()} (NodeArray)",
            MPVFormat.NodeMap => $"{Marshal.PtrToStructure<MPVNodeList>(NodeList).ToString()} (NodeMap)",
            MPVFormat.ByteArray => ByteArray.ToString(),
            _ => Marshal.PtrToStringUTF8(String)
        } ?? "";
    }
}