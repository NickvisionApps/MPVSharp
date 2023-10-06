using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Byte Array
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVByteArray
{
    /// <summary>
    /// Pointer to byte[]
    /// </summary>
    private nint _data;
    /// <summary>
    /// Array size
    /// </summary>
    public uint Size;
    
    /// <summary>
    /// Empty MPVByteArray
    /// </summary>
    public static MPVByteArray Empty { get; } = new MPVByteArray { Size = 0 };

    public static implicit operator byte[](MPVByteArray mba)
    {
        var result = new byte[mba.Size];
        Marshal.Copy(mba._data, result, 0, (int)mba.Size);
        return result;
    }
}