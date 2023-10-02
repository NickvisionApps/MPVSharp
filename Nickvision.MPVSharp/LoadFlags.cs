namespace Nickvision.MPVSharp;

/// <summary>
/// Flags for <see cref="Client.LoadFile"/> and <see cref="Client.LoadList"/> methods
/// </summary>
/// <remarks>
/// This is called flags to match terminology from MPV docs,
/// you can't use multiple values at once like with SeekFlags
/// </remarks>
public enum LoadFlags
{
    /// <summary>
    /// Stop playback and play new file immediately
    /// </summary>
    Replace = 0,
    /// <summary>
    /// Append the file to the playlist
    /// </summary>
    Append,
    /// <summary>
    /// Append the file, and if nothing is currently playing, start playback
    /// </summary>
    AppendPlay
}

/// <summary>
/// Extensions for LoadFlags
/// </summary>
public static class LoadFlagsExtensions
{
    /// <summary>
    /// Converts <see cref="LoadFlags"/> to a string acceptable by MPV
    /// </summary>
    public static string FlagsToString(this LoadFlags flags)
    {
        return flags switch
        {
            LoadFlags.Replace => "replace",
            LoadFlags.Append => "append",
            _ => "append-play"
        };
    }
}