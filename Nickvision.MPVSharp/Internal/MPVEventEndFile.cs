using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Data for EndFile event
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVEventEndFile
{
    /// <summary>
    /// Event reason
    /// </summary>
    public MPVEndFileReason Reason;
    /// <summary>
    /// Contains error code if reason is <see cref="MPVEndFileReason.Error"/>, or 0
    /// </summary>
    public MPVError Error;
    /// <summary>
    /// Playlist entry Id of the file that was being played
    /// </summary>
    public long PlaylistEntryId;
    /// <summary>
    /// If loading ended, because the playlist entry to be played was for example
    /// a playlist, and the current playlist entry is replaced with a number of
    /// other entries, this will be set to the playlist entry Id of the first inserted entry
    /// </summary>
    public long PlaylistInsertId;
    /// <summary>
    /// If loading ended, because the playlist entry to be played was for example
    /// a playlist, and the current playlist entry is replaced with a number of
    /// other entries, this will be set to the total number of inserted playlist entries
    /// </summary>
    public int PlaylistInsertNumEntries;
}