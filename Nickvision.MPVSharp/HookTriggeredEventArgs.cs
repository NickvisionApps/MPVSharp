namespace Nickvision.MPVSharp;

/// <summary>
/// Args for Hook event
/// </summary>
public class HookTriggeredEventArgs : EventArgs
{
    /// <summary>
    /// The hook name as passed to MPVClient.HookAdd()
    /// </summary>
    public string Name;
    /// <summary>
    /// Internal ID that must be passed to MPVClient.HookContinue()
    /// </summary>
    public ulong Id;

    /// <summary>
    /// Create args for Hook event
    /// </summary>
    /// <param name="name">The hook name as passed to MPVClient.HookAdd()</param>
    /// <param name="id">Internal ID that must be passed to MPVClient.HookContinue()</param>
    public HookTriggeredEventArgs(string name, ulong id)
    {
        Name = name;
        Id = id;
    }
}