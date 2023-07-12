using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp;

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
}