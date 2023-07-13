using Nickvision.MPVSharp.Internal;

namespace Nickvision.MPVSharp;

public class EventProperty {
    /// <summary>
    /// Property name
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// Property MPV format
    /// </summary>
    public MPVFormat Format { get; init; }
    /// <summary>
    /// Property data in Node
    /// </summary>
    public MPVNode? Node { get; init; }
    
    public EventProperty(MPVEventProperty prop)
    {
        Name = prop.Name;
        Format = prop.Format;
        if (Format != MPVFormat.None)
        {
            Node = MPVNode.FromIntPtr(prop.Data);
        }
    }
}