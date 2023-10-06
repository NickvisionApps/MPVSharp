using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for GetPropertyReply event
/// </summary>
public class GetPropertyReplyReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Reply userdata
    /// </summary>
    public ulong ReplyUserdata { get; init; }
    /// <summary>
    /// Property name
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// <see cref="Nickvision.MPVSharp.Node"/> holding data
    /// </summary>
    public Node Node { get; init; }

    /// <summary>
    /// Creates args for GetPropertyReply event
    /// </summary>
    /// <param name="replyUserdata">Reply Id</param>
    /// <param name="name">Property name</param>
    /// <param name="node"><see cref="Nickvision.MPVSharp.Node"/> holding data</param>
    public GetPropertyReplyReceivedEventArgs(ulong replyUserdata, string name, Node node)
    {
        ReplyUserdata = replyUserdata;
        Name = name;
        Node = node;
    }
}