using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Event Log Message structure
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVEventLogMessage
{
    /// <summary>
    /// Module prefix, sender of the message
    /// </summary>
    public string Prefix;
    /// <summary>
    /// Log level as string
    /// </summary>
    public string Level;
    /// <summary>
    /// Log message, ends with newline char
    /// </summary>
    public string Text;
    /// <summary>
    /// Message log level
    /// </summary>
    public MPVLogLevel LogLevel;
}