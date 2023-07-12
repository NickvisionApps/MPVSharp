namespace Nickvision.MPVSharp;

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