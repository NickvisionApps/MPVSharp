using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Data for Command event
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVEventCommand
{
    /// <summary>
    /// Result data of the command
    /// </summary>
    public MPVNode Result;
}