using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// General MPV functions
/// </summary>
public static partial class MPV
{
    [LibraryImport("mpv")]
    private static partial ulong mpv_client_api_version();
    [LibraryImport("mpv")]
    private static partial void mpv_free(nint data);
    
    /// <summary>
    /// Returns the API version the mpv source has been compiled with.
    /// </summary>
    public static ulong APIVersion => mpv_client_api_version();
    
    /// <summary>
    /// General function to deallocate memory returned by some of the API functions.
    /// Call this only if it's explicitly documented as allowed.
    /// </summary>
    public static void Free(nint data) => mpv_free(data);
}