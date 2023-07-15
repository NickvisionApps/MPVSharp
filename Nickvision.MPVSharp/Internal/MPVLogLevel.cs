namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Log Level (0 - disable logging, 70 - extremely noisy)
/// </summary>
public enum MPVLogLevel
{
    None = 0,
    Fatal = 10,
    Error = 20,
    Warn = 30,
    Info = 40,
    V = 50,
    Debug = 60,
    Trace = 70
}