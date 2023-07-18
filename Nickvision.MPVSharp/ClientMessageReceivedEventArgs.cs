using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for ClientMessage event
/// </summary>
public class ClientMessageReceivedEventArgs : EventArgs
{
    public string[] Args { get; init; }

    /// <summary>
    /// Create args for ClientMessage event
    /// </summary>
    /// <param name="args">Arbitraty arguments chosen by the sender of the message</param>
    public ClientMessageReceivedEventArgs(string[] args)
    {
        Args = args;
    }
}