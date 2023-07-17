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
    /// Get EventLogMessage of LogMessage event
    /// </summary>
    public MPVEventLogMessage? GetEventLogMessage()
    {
        if (Id != MPVEventId.LogMessage)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventLogMessage>(_data);
    }

    /// <summary>
    /// Get MPVEventProperty of PropertyChange or GetPropertyReply event
    /// </summary>
    public MPVEventProperty? GetEventProperty()
    {
        if ((Id != MPVEventId.PropertyChange && Id != MPVEventId.GetPropertyReply) || Error < (int)MPVError.Success)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventProperty>(_data);
    }

    public MPVEventCommand? GetCommandResult()
    {
        if (Id != MPVEventId.CommandReply)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventCommand>(_data);
    }

    public MPVEventStartFile? GetStartFile()
    {
        if (Id != MPVEventId.StartFile)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventStartFile>(_data);
    }

    public MPVEventEndFile? GetEndFile()
    {
        if (Id != MPVEventId.EndFile)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventEndFile>(_data);
    }

    public string[]? GetClientMessage()
    {
        if (Id != MPVEventId.ClientMessage)
        {
            return null;
        }
        return MPVEventClientMessage.GetStringArgs(Marshal.PtrToStructure<MPVEventClientMessage>(_data));
    }

    public MPVEventHook? GetHook()
    {
        if (Id != MPVEventId.Hook)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventHook>(_data);
    }
}