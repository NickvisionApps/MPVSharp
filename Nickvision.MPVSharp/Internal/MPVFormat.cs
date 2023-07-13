namespace Nickvision.MPVSharp;

/// <summary>
/// MPV data format
/// </summary>
public enum MPVFormat
{
    None = 0,
    String,
    OsdString,
    Flag,
    Int64,
    Double,
    Node,
    NodeArray,
    NodeMap,
    ByteArray
}