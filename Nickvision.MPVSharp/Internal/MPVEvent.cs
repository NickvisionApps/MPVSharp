using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Event structure
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct MPVEvent
{
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial string mpv_event_name(MPVEventId id);

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

    /// <summary>
    /// Get string describing the event
    /// </summary>
    public string Name => mpv_event_name(Id);

    /// <summary>
    /// Get MPVEventProperty of PropertyChange or GetPropertyReply event
    /// </summary>
    public MPVEventProperty? GetEventProperty()
    {
        if (Id != MPVEventId.PropertyChange && Id != MPVEventId.GetPropertyReply)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventProperty>(_data);
    }
}