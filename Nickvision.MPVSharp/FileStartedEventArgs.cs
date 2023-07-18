using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for StartFile event
/// </summary>
public class FileStartedEventArgs : EventArgs
{
    public long PlaylistEntryId;

    /// <summary>
    /// Create args for StartFile event
    /// </summary>
    /// <param name="playlistEntryId">Index of file in playlist</param>
    public FileStartedEventArgs(long playlistEntryId)
    {
        PlaylistEntryId = playlistEntryId;
    }
}