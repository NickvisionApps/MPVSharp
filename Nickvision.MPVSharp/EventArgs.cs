using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for PropertyChange event
/// </summary>
public class PropertyChangedEventArgs : EventArgs
{
    public string Name;
    public MPVFormat Format;
    public object Data;

    public PropertyChangedEventArgs(string name, MPVFormat format, object data)
    {
        Name = name;
        Format = format;
        Data = data;
    }
}