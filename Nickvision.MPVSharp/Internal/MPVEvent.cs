using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Event structure
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct MPVEvent
{
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
    private static partial string mpv_event_name(MPVEventId id);
    [LibraryImport("mpv", StringMarshalling = StringMarshalling.Utf8)]
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
    /// The string describing the event
    /// </summary>
    public string Name => mpv_event_name(Id);

    /// <summary>
    /// The EventLogMessage of LogMessage event
    /// </summary>
    public MPVEventLogMessage? EventLogMessage
    {
        get
        {
            if (Id != MPVEventId.LogMessage)
            {
                return null;
            }
            return Marshal.PtrToStructure<MPVEventLogMessage>(_data);
        }
    }

    /// <summary>
    /// The MPVEventProperty of PropertyChange or GetPropertyReply event
    /// </summary>
    public MPVEventProperty? EventProperty
    {
        get
        {
            if ((Id != MPVEventId.PropertyChange && Id != MPVEventId.GetPropertyReply) || Error < (int)MPVError.Success)
            {
                return null;
            }
            return Marshal.PtrToStructure<MPVEventProperty>(_data);
        }
    }

    /// <summary>
    /// The MPVEventCommand of CommandReply event
    /// </summary>
    public MPVEventCommand? CommandResult
    {
        get
        {
            if (Id != MPVEventId.CommandReply)
            {
                return null;
            }
            return Marshal.PtrToStructure<MPVEventCommand>(_data);
        }
    }

    /// <summary>
    /// The MPVEventStartFile of StartFile event
    /// </summary>
    public MPVEventStartFile? StartFile
    {
        get
        {
            if (Id != MPVEventId.StartFile)
            {
                return null;
            }
            return Marshal.PtrToStructure<MPVEventStartFile>(_data);
        }
    }

    /// <summary>
    /// The MPVEventEndFile of EndFile event
    /// </summary>
    public MPVEventEndFile? EndFile
    {
        get
        {
            if (Id != MPVEventId.EndFile)
            {
                return null;
            }
            return Marshal.PtrToStructure<MPVEventEndFile>(_data);
        }
    }

    /// <summary>
    /// The client message of ClientMessage event
    /// </summary>
    public string[]? ClientMessage
    {
        get
        {
            if (Id != MPVEventId.ClientMessage)
            {
                return null;
            }
            return MPVEventClientMessage.GetStringArgs(Marshal.PtrToStructure<MPVEventClientMessage>(_data));
        }
    }

    /// <summary>
    /// The MPVEventHook of Hook event
    /// </summary>
    public MPVEventHook? Hook
    {
        get
        {
            if (Id != MPVEventId.Hook)
            {
                return null;
            }
            return Marshal.PtrToStructure<MPVEventHook>(_data);
        }
    }

    /// <summary>
    /// Convert given MPVEvent to MPVNode
    /// </summary>
    /// <param name="node">A node where to write result to</param>
    /// <param name="e">Event</param>
    /// <returns>Error code</returns>
    public static MPVError ToNode(out MPVNode node, MPVEvent e) => mpv_event_to_node(out node, ref e);
}