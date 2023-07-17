using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

[StructLayout(LayoutKind.Sequential)]
public struct MPVEventClientMessage {
    /// <summary>
    /// Number of arguments
    /// </summary>
    public int Num;
    /// <summary>
    /// Pointer to arbitraty arguments chosen by the sender of the message
    /// </summary>
    public nint ArgsPtr;
    
    /// <summary>
    /// Get string args from args pointer of a message
    /// </summary>
    public static string[] GetStringArgs(MPVEventClientMessage m)
    {
        var ptr = m.ArgsPtr;
        var result = new string[m.Num];
        for (var i = 0; i < m.Num; i++)
        {
            result[i] = Marshal.PtrToStringUTF8(Marshal.ReadIntPtr(m.ArgsPtr + i * nint.Size))!;
        }
        return result;
    }
}