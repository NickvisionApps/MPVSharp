using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for PropertyChange event
/// </summary>
public class PropertyChangedEventArgs : EventArgs
{
    /// <summary>
    /// Property name
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// <see cref="Nickvision.MPVSharp.Node"/> holding data
    /// </summary>
    public Node Node { get; init; }

    /// <summary>
    /// Creates args for PropertyChange event
    /// </summary>
    /// <param name="name">Property name</param>
    /// <param name="node"><see cref="Nickvision.MPVSharp.Node"/> holding data</param>
    public PropertyChangedEventArgs(string name, Node node)
    {
        Name = name;
        Node = node;
    }
}