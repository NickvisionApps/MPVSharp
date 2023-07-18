using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Data for StartFile event
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVEventStartFile
{
    /// <summary>
    /// Playlist entry Id of the file being loaded now.
    /// </summary>
    public long PlaylistEntryId;
}