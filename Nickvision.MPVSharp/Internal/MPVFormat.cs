namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Data format for options and properties.
/// </summary>
public enum MPVFormat
{
    /// <summary>
    /// Invalid.
    /// </summary>
    None = 0,
    /// <summary>
    /// The basic type is string (char* in C).
    /// It returns the raw property string, like
    /// using ${=property} in input.conf
    /// </summary>
    String,
    /// <summary>
    /// The basic type is string (char* in C).
    /// It returns the OSD property string, like
    /// using ${property} in input.conf
    /// </summary>
    OSDString,
    /// <summary>
    /// The basic type is int (0 or 1).
    /// </summary>
    Flag,
    /// <summary>
    /// The basic type is long (int64_t in C).
    /// </summary>
    Int64,
    /// <summary>
    /// The basic type is double.
    /// </summary>
    Double,
    /// <summary>
    /// The type is <see cref="MPVNode"/>.
    /// </summary>
    Node,
    /// <summary>
    /// Used with <see cref="MPVNode"/> only.
    /// </summary>
    NodeArray,
    /// <summary>
    /// Used with <see cref="MPVNode"/> only.
    /// </summary>
    NodeMap,
    /// <summary>
    /// A raw byte[], only used with <see cref="MPVNode"/>.
    /// </summary>
    ByteArray
}