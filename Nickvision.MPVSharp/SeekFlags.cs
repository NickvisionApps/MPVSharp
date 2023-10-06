using System;
using System.Text;

namespace Nickvision.MPVSharp;

/// <summary>
/// Flags for <see cref="Client.Seek"/> method
/// </summary>
[Flags]
public enum SeekFlags
{
    /// <summary>
    /// Seek relative to current position
    /// </summary>
    /// <remarks>Incompatible with Absolute</remarks>
    Relative = 1,
    /// <summary>
    /// Seek to a given position
    /// </summary>
    /// <remarks>Incompatible with Relative</remarks>
    Absolute = 2,
    /// <summary>
    /// Treat seek value as percent, not seconds
    /// </summary>
    Percent = 4,
    /// <summary>
    /// Fast seek, restart at keyframe boundaries
    /// </summary>
    /// <remarks>Incompatible with Exact</remarks>
    Keyframes = 8,
    /// <summary>
    /// Slow seek, do precise seek
    /// </summary>
    /// <remarks>Incompatible with Keyframes</remarks>
    Exact = 16
}

/// <summary>
/// Extensions for SeekFlags
/// </summary>
public static class SeekFlagsExtensions
{
    /// <summary>
    /// Converts <see cref="SeekFlags"/> to a string acceptable by MPV
    /// </summary>
    public static string FlagsToString(this SeekFlags flags)
    {
        var result = new StringBuilder();
        result.Append(flags.HasFlag(SeekFlags.Absolute) ? "absolute" : "relative");
        if (flags.HasFlag(SeekFlags.Percent))
        {
            result.Append("-percent");
        }
        if (flags.HasFlag(SeekFlags.Keyframes))
        {
            result.Append("+keyframes");
        }
        else if (flags.HasFlag(SeekFlags.Exact))
        {
            result.Append("+exact");
        }
        return result.ToString();
    }
}