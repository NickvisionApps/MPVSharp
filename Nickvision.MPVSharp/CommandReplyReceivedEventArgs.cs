using Nickvision.MPVSharp.Internal;
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
    /// Command result
    /// </summary>
    public MPVNode Result { get; init; }

    /// <summary>
    /// Create args for CommandReply event
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="error">Error code</param>
    /// <param name="result">Command result</param>
    public CommandReplyReceivedEventArgs(ulong replyUserdata, int error, MPVNode result)
    {
        ReplyUserdata = replyUserdata;
        Error = error;
        Result = result;
    }
}