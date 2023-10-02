using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Data for ClientMessage event
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVEventClientMessage
{
    /// <summary>
    /// Number of arguments
    /// </summary>
    public int Num;
    /// <summary>
    /// Pointer to arbitrary arguments chosen by the sender of the message
    /// </summary>
    private nint _argsPtr;
    
    /// <summary>
    /// Gets string args from args pointer of a message
    /// </summary>
    public static string[] GetStringArgs(MPVEventClientMessage m)
    {
        var result = new string[m.Num];
        for (var i = 0; i < m.Num; i++)
        {
            result[i] = Marshal.PtrToStringUTF8(Marshal.ReadIntPtr(m._argsPtr + i * nint.Size))!;
        }
        return result;
    }
}