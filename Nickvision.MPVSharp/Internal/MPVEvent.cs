using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Event structure
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVEvent
{
    /// <summary>
    /// Event Id
    /// </summary>
    public MPVEventId Id;
    /// <summary>
    /// Event error code
    /// </summary>
    public int Error;
    /// <summary>
    /// Reply Id
    /// </summary>
    public ulong ReplyUserdata;
    /// <summary>
    /// Event data
    /// </summary>
    private nint _data;

    public MPVEventProperty? GetEventProperty()
    {
        if (Id != MPVEventId.PropertyChange && Id != MPVEventId.GetPropertyReply)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventProperty>(_data);
    }
}