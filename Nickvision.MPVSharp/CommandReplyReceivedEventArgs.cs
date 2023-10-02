using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for CommandReply event
/// </summary>
public class CommandReplyReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Reply userdata
    /// </summary>
    public ulong ReplyUserdata { get; init; }
    /// <summary>
    /// Error code
    /// </summary>
    public int Error { get; init; }
    /// <summary>
    /// <see cref="Nickvision.MPVSharp.Node"/> with command result
    /// </summary>
    public Node Result { get; init; }

    /// <summary>
    /// Creates args for CommandReply event
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="error">Error code</param>
    /// <param name="result"><see cref="Nickvision.MPVSharp.Node"/> with command result</param>
    public CommandReplyReceivedEventArgs(ulong replyUserdata, int error, Node result)
    {
        ReplyUserdata = replyUserdata;
        Error = error;
        Result = result;
    }
}