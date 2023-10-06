using Nickvision.MPVSharp.Internal;
using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for EndFile event
/// </summary>
public class FileEndedEventArgs : EventArgs
{
    /// <summary>
    /// File ending reason
    /// </summary>
    public MPVEndFileReason Reason { get; init; }
    /// <summary>
    /// Contains error code if reason is MPVEndFileReason.Error, or 0
    /// </summary>
    public MPVError Error { get; init; }
    /// <summary>
    /// Playlist entry Id of the file that was being played
    /// </summary>
    public long PlaylistEntryId { get; init; }
    /// <summary>
    /// If loading ended, because the playlist entry to be played was for example
    /// a playlist, and the current playlist entry is replaced with a number of
    /// other entries, this will be set to the playlist entry Id of the first inserted entry
    /// </summary>
    public long PlaylistInsertId { get; init; }
    /// <summary>
    /// If loading ended, because the playlist entry to be played was for example
    /// a playlist, and the current playlist entry is replaced with a number of
    /// other entries, this will be set to the total number of inserted playlist entries
    /// </summary>
    public int PlaylistInsertNumEntries { get; init; }

    /// <summary>
    /// Creates args for EndFile event
    /// </summary>
    /// <param name="reason">File ending reason</param>
    /// <param name="error">Error code for error reason</param>
    /// <param name="playlistEntryId">Playlist entry Id of the file that was being played</param>
    /// <param name="playlistInsertId">Playlist entry Id of first inserted item</param>
    /// <param name="playlistInsertNumEntries">Number of entries in inserted playlist</param>
    public FileEndedEventArgs(MPVEndFileReason reason, MPVError error, long playlistEntryId, long playlistInsertId, int playlistInsertNumEntries)
    {
        Reason = reason;
        Error = error;
        PlaylistEntryId = playlistEntryId;
        PlaylistEntryId = playlistEntryId;
        PlaylistInsertId = playlistInsertId;
        PlaylistInsertNumEntries = playlistInsertNumEntries;
    }
}