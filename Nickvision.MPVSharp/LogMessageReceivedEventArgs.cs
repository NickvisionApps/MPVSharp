using Nickvision.MPVSharp.Internal;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for LogMessage event
/// </summary>
public class LogMessageReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Module prefix, sender of the message
    /// </summary>
    public string Prefix;
    /// <summary>
    /// Log message, ends with newline char
    /// </summary>
    public string Text;
    /// <summary>
    /// Message log level
    /// </summary>
    public MPVLogLevel LogLevel;
    
    /// <summary>
    /// Create args for LogMessage event
    /// </summary>
    /// <param name="prefix">Module prefix</param>
    /// <param name="text">Message text</param>
    /// <param name="logLevel">Message log level</param>
    public LogMessageReceivedEventArgs(string prefix, string text, MPVLogLevel logLevel)
    {
        Prefix = prefix;
        Text = text;
        LogLevel = logLevel;
    }
}