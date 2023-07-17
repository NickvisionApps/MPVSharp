namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Log Level (0 - disable logging, 70 - extremely noisy)
/// </summary>
public enum MPVLogLevel
{
    /// <summary>
    /// Disable all messages
    /// </summary>
    None = 0,
    /// <summary>
    /// Critical/aborting errors
    /// </summary>
    Fatal = 10,
    /// <summary>
    /// Simple errors
    /// </summary>
    Error = 20,
    /// <summary>
    /// Possible problems
    /// </summary>
    Warn = 30,
    /// <summary>
    /// Informational message
    /// </summary>
    Info = 40,
    /// <summary>
    /// Noisy informational message
    /// </summary>
    V = 50,
    /// <summary>
    /// Very noisy technical information
    /// </summary>
    Debug = 60,
    /// <summary>
    /// Extremely noisy
    /// </summary>
    Trace = 70
}