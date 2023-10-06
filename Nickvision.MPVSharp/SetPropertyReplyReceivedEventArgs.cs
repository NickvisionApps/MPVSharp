using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for SetPropertyReply event
/// </summary>
public class SetPropertyReplyReceivedEventArgs : EventArgs
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
    /// Creates args for SetPropertyReply event
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="error">Error code</param>
    public SetPropertyReplyReceivedEventArgs(ulong replyUserdata, int error)
    {
        ReplyUserdata = replyUserdata;
        Error = error;
    }
}