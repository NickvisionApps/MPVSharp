using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV error code
/// </summary>
public enum MPVError {
    Generic = -20,
    NotImplemented,
    Unsupported,
    UnknownFormat,
    NothingToPlay,
    VOInitFailed,
    AOInitFailed,
    LoadingFailed,
    Command,
    PropertyError,
    PropertyUnavailable,
    PropertyFormat,
    PropertyNotFound,
    OptionError,
    OptionFormat,
    OptionNotFound,
    InvalidParameter,
    Uninitialized,
    NoMem,
    QueueFull,
    Success
}

public static partial class MPVErrorExtensions
{
    [LibraryImport("libmpv.so.2")]
    private static partial nint mpv_error_string(MPVError error);

    public static string ToMPVErrorString(this MPVError error)
    {
        var ptr = mpv_error_string(error);
        return Marshal.PtrToStringUTF8(ptr)!;
    }
}