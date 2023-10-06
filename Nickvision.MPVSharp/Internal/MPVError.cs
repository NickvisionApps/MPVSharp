using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV error codes
/// </summary>
public enum MPVError
{
    /// <summary>
    /// Unspecified error.
    /// </summary>
    Generic = -20,
    /// <summary>
    /// The API function which was called is a stub only.
    /// </summary>
    NotImplemented,
    /// <summary>
    /// Generic error for signaling that certain system requirements are not fulfilled.
    /// </summary>
    Unsupported,
    /// <summary>
    /// When trying to load the file, the file format could not be determined, or the file was too broken to open it.
    /// </summary>
    UnknownFormat,
    /// <summary>
    /// There was no audio or video data to play.
    /// </summary>
    NothingToPlay,
    /// <summary>
    /// Initializing the video output failed.
    /// </summary>
    VOInitFailed,
    /// <summary>
    /// Initializing the audio output failed.
    /// </summary>
    AOInitFailed,
    /// <summary>
    /// Generic error on loading (usually used with MPVEventId.EndFile).
    /// </summary>
    LoadingFailed,
    /// <summary>
    /// General error when running a command with MPVClient.Command and similar.
    /// </summary>
    Command,
    /// <summary>
    /// Error setting or getting a property.
    /// </summary>
    PropertyError,
    /// <summary>
    /// The property exists, but is not available.
    /// </summary>
    PropertyUnavailable,
    /// <summary>
    /// Trying to set or get a property using an unsupported MPVFormat.
    /// </summary>
    PropertyFormat,
    /// <summary>
    /// The accessed property doesn't exist.
    /// </summary>
    PropertyNotFound,
    /// <summary>
    /// Setting the option failed.
    /// </summary>
    OptionError,
    /// <summary>
    /// Trying to set an option using an unsupported MPVFormat.
    /// </summary>
    OptionFormat,
    /// <summary>
    /// Trying to set an option that doesn't exist.
    /// </summary>
    OptionNotFound,
    /// <summary>
    /// Generic catch-all error if a parameter is set to an invalid or unsupported value.
    /// </summary>
    InvalidParameter,
    /// <summary>
    /// The mpv core wasn't configured and initialized yet.
    /// </summary>
    Uninitialized,
    /// <summary>
    /// Memory allocation failed.
    /// </summary>
    NoMem,
    /// <summary>
    /// The event ringbuffer is full. This means the client is choked, and can't receive any events.
    /// </summary>
    QueueFull,
    /// <summary>
    /// No error happened (used to signal successful operation).
    /// </summary>
    Success
}

public static partial class MPVErrorExtensions
{
    [LibraryImport("mpv")]
    private static partial nint mpv_error_string(MPVError error);

    /// <summary>
    /// Returns a string describing the error.
    /// </summary>
    public static string ToMPVErrorString(this MPVError error)
    {
        var ptr = mpv_error_string(error);
        return Marshal.PtrToStringUTF8(ptr)!;
    }
}