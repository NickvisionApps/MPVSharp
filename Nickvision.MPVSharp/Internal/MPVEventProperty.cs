using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

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
    /// Property data pointer
    /// </summary>
    private nint _data;

    public object? GetData()
    {
        return Format switch
        {
            MPVFormat.String or MPVFormat.OsdString => Marshal.PtrToStringUTF8(_data),
            MPVFormat.Flag => Marshal.ReadInt32(_data) == 1,
            MPVFormat.Int64 => Marshal.ReadInt64(_data),
            MPVFormat.Double => 0.0,
            MPVFormat.Node => Marshal.PtrToStructure<MPVNode>(_data),
            _ => null
        };
    }
}