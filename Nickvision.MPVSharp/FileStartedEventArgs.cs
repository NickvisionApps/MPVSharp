using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for StartFile event
/// </summary>
public class FileStartedEventArgs : EventArgs
{
    /// <summary>
    /// Index of file in playlist
    /// </summary>
    public long PlaylistEntryId { get; init; }

    /// <summary>
    /// Creates args for StartFile event
    /// </summary>
    /// <param name="playlistEntryId">Index of file in playlist</param>
    public FileStartedEventArgs(long playlistEntryId)
    {
        PlaylistEntryId = playlistEntryId;
    }
}