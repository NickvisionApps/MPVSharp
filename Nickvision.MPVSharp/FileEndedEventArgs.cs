using Nickvision.MPVSharp.Internal;
using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// Args for EndFile event
/// </summary>
public class FileEndedEventArgs : EventArgs
{
    public MPVEndFileReason Reason;
    public MPVError Error;
    public long PlaylistEntryId;
    public long PlaylistInsertId;
    public int PlaylistInsertNumEntries;

    /// <summary>
    /// Create args for EndFile event
    /// </summary>
    /// <param name="reason">File ending reason</param>
    /// <param name="error">Error code for error reason</param>
    /// <param name="playlistEntryId">Index of file in playlist</param>
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