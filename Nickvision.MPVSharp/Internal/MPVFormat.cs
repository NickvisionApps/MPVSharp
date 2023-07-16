namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV data format
/// </summary>
public enum MPVFormat
{
    None = 0,
    String,
    OSDString,
    Flag,
    Int64,
    Double,
    Node,
    NodeArray,
    NodeMap,
    ByteArray
}