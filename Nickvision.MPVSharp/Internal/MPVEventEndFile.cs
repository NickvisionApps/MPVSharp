using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

[StructLayout(LayoutKind.Sequential)]
public struct MPVEventEndFile {
    public MPVEndFileReason Reason;
    public MPVError Error;
    public long PlaylistEntryId;
    public long PlaylistInsertId;
    public int PlaylistInsertNumEntries;
}