namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Possible reasons for EndFile event
/// </summary>
public enum MPVEndFileReason
{
    /// <summary>
    /// The end of file was reached.
    /// </summary>
    EOF = 0,
    /// <summary>
    /// Playback was stopped by an external action (e.g. playlist controls).
    /// </summary>
    Stop = 2,
    /// <summary>
    /// Playback was stopped by the quit command or player shutdown.
    /// </summary>
    Quit,
    /// <summary>
    /// Some kind of error happened that lead to playback abort.
    /// </summary>
    Error,
    /// <summary>
    /// The file was a playlist or similar
    /// </summary>
    Redirect
}