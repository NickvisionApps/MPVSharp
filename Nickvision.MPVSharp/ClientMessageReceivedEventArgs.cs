using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for ClientMessage event
/// </summary>
public class ClientMessageReceivedEventArgs : EventArgs
{
    /// <summary>
    /// String array with client message arguments
    /// </summary>
    public string[] Args { get; init; }

    /// <summary>
    /// Creates args for ClientMessage event
    /// </summary>
    /// <param name="args">Arbitrary arguments chosen by the sender of the message</param>
    public ClientMessageReceivedEventArgs(string[] args)
    {
        Args = args;
    }
}