using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp;

[StructLayout(LayoutKind.Sequential)]
public struct MPVEventProperty
{
    public string Name;
    public MPVFormat Format;
    public nint Data;

    public MPVEventProperty(string name = "", MPVFormat format = MPVFormat.None, nint data = 0)
    {
        Name = name;
        Format = format;
        Data = data;
    }
}