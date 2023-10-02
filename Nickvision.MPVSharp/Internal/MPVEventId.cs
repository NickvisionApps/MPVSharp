namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Possible event Ids
/// </summary>
public enum MPVEventId
{
    /// <summary>
    /// Nothing happened
    /// </summary>
    None = 0,
    /// <summary>
    /// Happens when the player quits
    /// </summary>
    Shutdown,
    /// <summary>
    /// Log message received
    /// </summary>
    LogMessage,
    /// <summary>
    /// Reply to <see cref="MPVClient.GetPropertyAsync"/>
    /// </summary>
    GetPropertyReply,
    /// <summary>
    /// Reply to <see cref="MPVClient.SetPropertyAsync(ulong,string,int)"/>
    /// </summary>
    SetPropertyReply,
    /// <summary>
    /// Reply to <see cref="MPVClient.CommandAsync"/>
    /// </summary>
    CommandReply,
    /// <summary>
    /// Notification before playback start of a file (before the file is loaded)
    /// </summary>
    StartFile,
    /// <summary>
    /// Notification after playback end (after the file was unloaded)
    /// </summary>
    EndFile,
    /// <summary>
    /// Notification when the file has been loaded
    /// </summary>
    FileLoaded,
    /// <summary>
    /// Triggered by the script-message input command
    /// </summary>
    ClientMessage = 16,
    /// <summary>
    /// Happens after video changed in some way
    /// </summary>
    VideoReconfig,
    /// <summary>
    /// Happens after audio changed in some way
    /// </summary>
    AudioReconfig,
    /// <summary>
    /// Happens when a seek was initiated
    /// </summary>
    Seek = 20,
    /// <summary>
    /// There was a discontinuity of some sort (like a seek), and playback was reinitialized
    /// </summary>
    PlaybackRestart,
    /// <summary>
    /// Event sent due to <see cref="MPVClient.ObserveProperty"/>
    /// </summary>
    PropertyChange,
    /// <summary>
    /// Happens if the internal per-mpv_handle ringbuffer overflows
    /// </summary>
    QueueOverflow = 24,
    /// <summary>
    /// Triggered if a hook handler was registered with <see cref="MPVClient.HookAdd"/>
    /// </summary>
    Hook
}