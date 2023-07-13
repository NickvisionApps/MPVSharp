using Nickvision.MPVSharp.Internal;

namespace Nickvision.MPVSharp;

public class ClientEvent {
    public MPVEventId Id { get; init; }
    public int Error { get; init; }
    public ulong ReplyUserdata { get; init; }
    private readonly nint _data;
    
    public ClientEvent(MPVEvent mpvEvent)
    {
        Id = mpvEvent.Id;
        Error = mpvEvent.Error;
        ReplyUserdata = mpvEvent.ReplyUserdata;
        _data = mpvEvent.Data;
    }
    
    public EventProperty GetEventProperty()
    {
        if (Id != MPVEventId.PropertyChange && Id != MPVEventId.GetPropertyReply)
        {
            throw new ClientException(MPVError.PropertyError);
        }
        return new EventProperty(MPVEventProperty.FromIntPtr(_data));
    }
}