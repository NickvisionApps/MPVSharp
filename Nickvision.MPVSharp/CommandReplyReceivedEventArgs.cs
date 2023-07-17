using Nickvision.MPVSharp.Internal;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for CommandReply event
/// </summary>
public class CommandReplyReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Reply userdata
    /// </summary>
    public ulong ReplyUserdata;
    /// <summary>
    /// Error code
    /// </summary>
    public int Error;
    /// <summary>
    /// Command result
    /// </summary>
    public MPVNode Result;

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