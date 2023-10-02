using Nickvision.MPVSharp.Internal;
using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for Hook event
/// </summary>
public class HookTriggeredEventArgs : EventArgs
{
    /// <summary>
    /// The hook name as passed to MPVClient.HookAdd()
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// Internal ID that must be passed to MPVClient.HookContinue()
    /// </summary>
    public ulong Id { get; init; }

    /// <summary>
    /// Creates args for Hook event
    /// </summary>
    /// <param name="name">The hook name as passed to <see cref="MPVClient.HookAdd"/></param>
    /// <param name="id">Internal ID that must be passed to <see cref="MPVClient.HookContinue"/></param>
    public HookTriggeredEventArgs(string name, ulong id)
    {
        Name = name;
        Id = id;
    }
}