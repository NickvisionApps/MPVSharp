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
    [LibraryImport("libmpv.so.2", StringMarshalling = StringMarshalling.Utf8)]
    private static partial MPVError mpv_event_to_node(out MPVNode node, ref MPVEvent e);

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
    /// Pointer to event data
    /// </summary>
    private nint _data;

    /// <summary>
    /// Get string describing the event
    /// </summary>
    /// <returns>Event name</returns>
    public string GetName() => mpv_event_name(Id);

    /// <summary>
    /// Get EventLogMessage of LogMessage event
    /// </summary>
    /// <returns>MPVEventLogMessage or null if wrong event Id</returns>
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
    /// <returns>MPVEventProperty or null if wrong event Id</returns>
    public MPVEventProperty? GetEventProperty()
    {
        if ((Id != MPVEventId.PropertyChange && Id != MPVEventId.GetPropertyReply) || Error < (int)MPVError.Success)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventProperty>(_data);
    }

    /// <summary>
    /// Get MPVEventCommand of CommandReply event
    /// </summary>
    /// <returns>MPVEventCommand or null if wrong event Id</returns>
    public MPVEventCommand? GetCommandResult()
    {
        if (Id != MPVEventId.CommandReply)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventCommand>(_data);
    }

    /// <summary>
    /// Get MPVEventStartFile of StartFile event
    /// </summary>
    /// <returns>MPVEventStartFile or null if wrong event Id</returns>
    public MPVEventStartFile? GetStartFile()
    {
        if (Id != MPVEventId.StartFile)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventStartFile>(_data);
    }

    /// <summary>
    /// Get MPVEventEndFile of EndFile event
    /// </summary>
    /// <returns>MPVEventEndFile or null if wrong event Id</returns>
    public MPVEventEndFile? GetEndFile()
    {
        if (Id != MPVEventId.EndFile)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventEndFile>(_data);
    }

    /// <summary>
    /// Get client message of ClientMessage event
    /// </summary>
    /// <returns>Array of string args or null if wrong event Id</returns>
    public string[]? GetClientMessage()
    {
        if (Id != MPVEventId.ClientMessage)
        {
            return null;
        }
        return MPVEventClientMessage.GetStringArgs(Marshal.PtrToStructure<MPVEventClientMessage>(_data));
    }

    /// <summary>
    /// Get MPVEventHook of Hook event
    /// </summary>
    /// <returns>MPVEventHook or null if wrong event Id</returns>
    public MPVEventHook? GetHook()
    {
        if (Id != MPVEventId.Hook)
        {
            return null;
        }
        return Marshal.PtrToStructure<MPVEventHook>(_data);
    }

    /// <summary>
    /// Convert given MPVEvent to MPVNode
    /// </summary>
    /// <param name="node">A node where to write result to</param>
    /// <param name="e">Event</param>
    /// <returns>Error code</returns>
    public static MPVError ToNode(out MPVNode node, MPVEvent e) => mpv_event_to_node(out node, ref e);
}