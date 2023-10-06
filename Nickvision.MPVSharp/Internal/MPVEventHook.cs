using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Data for Hook event
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVEventHook
{
    /// <summary>
    /// The hook name as passed to <see cref="MPVClient.HookAdd"/>
    /// </summary>
    public string Name;
    /// <summary>
    /// Internal ID that must be passed to <see cref="MPVClient.HookContinue"/>
    /// </summary>
    public ulong Id;
}