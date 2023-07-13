namespace Nickvision.MPVSharp;

/// <summary>
/// Args for PropertyChange event
/// </summary>
public class PropertyChangedEventArgs : EventArgs
{
    /// <summary>
    /// Property name
    /// </summary>
    public string Name;
    /// <summary>
    /// MPVNode holding data
    /// </summary>
    public MPVNode? Node;

    /// <summary>
    /// Creates args for PropertyChange event
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="node">Node holding data</param>
    public PropertyChangedEventArgs(string name, MPVNode? node)
    {
        Name = name;
        Node = node;
    }
}