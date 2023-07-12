using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp;

[StructLayout(LayoutKind.Sequential)]
public struct MPVEvent
{
    public MPVEventId Id;
    public int Error;
    public ulong ReplyUserdata;
    public nint Data;

    public MPVEvent(MPVEventId id = MPVEventId.None, int error = 0, ulong replyUserdata = 0, nint data = 0)
    {
        Id = id;
        Error = error;
        ReplyUserdata = replyUserdata;
        Data = data;
    }
}