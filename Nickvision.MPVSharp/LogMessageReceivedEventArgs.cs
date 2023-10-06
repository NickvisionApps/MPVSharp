using Nickvision.MPVSharp.Internal;
using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for LogMessage event
/// </summary>
public class LogMessageReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Module prefix, sender of the message
    /// </summary>
    public string Prefix { get; init; }
    /// <summary>
    /// Log message, ends with newline char
    /// </summary>
    public string Text { get; init; }
    /// <summary>
    /// Message log level
    /// </summary>
    public MPVLogLevel LogLevel { get; init; }
    
    /// <summary>
    /// Creates args for LogMessage event
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