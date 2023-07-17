namespace Nickvision.MPVSharp.Internal;

public enum MPVEventId {
    None = 0,
    Shutdown,
    LogMessage,
    GetPropertyReply,
    SetPropertyReply,
    CommandReply,
    StartFile,
    EndFile,
    FileLoaded,
    ClientMessage = 16,
    VideoReconfig,
    AudioReconfig,
    Seek = 20,
    PlaybackRestart,
    PropertyChange,
    QueueOverflow = 24,
    Hook
}