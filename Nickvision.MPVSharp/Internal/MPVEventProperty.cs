using System;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Data for PropertyChange and GetPropertyReply events
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

    /// <summary>
    /// Returns data object
    /// </summary>
    public object? GetData()
    {
        return Format switch
        {
            MPVFormat.String or MPVFormat.OSDString => Marshal.PtrToStringUTF8(_data),
            MPVFormat.Flag => Marshal.ReadInt32(_data) == 1,
            MPVFormat.Int64 => Marshal.ReadInt64(_data),
            MPVFormat.Double => ToDouble(_data),
            MPVFormat.Node => Marshal.PtrToStructure<MPVNode>(_data),
            _ => null
        };
    }

    /// <summary>
    /// Converts from pointer to double
    /// </summary>
    /// <param name="ptr">Pointer to data</param>
    /// <returns>Double object</returns>
    private double ToDouble(nint ptr)
    {
        var ba = new byte[sizeof(double)];
        for (var i = 0; i < ba.Length; i++)
        {
            ba[i] = Marshal.ReadByte(ptr, i);
        }
        return BitConverter.ToDouble(ba, 0);
    }
}
