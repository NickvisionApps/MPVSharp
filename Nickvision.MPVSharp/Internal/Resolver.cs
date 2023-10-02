using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Libraries import resolver
/// </summary>
public static class Resolver
{
    /// <summary>
    /// Dictionary containing pointers to native libraries
    /// </summary>
    private static Dictionary<string, nint> _libraries = new Dictionary<string, nint>();

    /// <summary>
    /// Sets import resolver if needed
    /// </summary>
    public static void SetResolver()
    {
        if (_libraries.Count == 0)
        {
            NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), LibraryImportResolver);
        }
    }
    
    /// <summary>
    /// Resolves native libraries
    /// </summary>
    /// <param name="libraryName">The string representing a library</param>
    /// <param name="assembly">The assembly loading a native library</param>
    /// <param name="searchPath">The search path</param>
    private static nint LibraryImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (_libraries.TryGetValue(libraryName, out var lib))
        {
            return lib;
        }
        var filename = "";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            filename = libraryName switch
            {
                "mpv" => "libmpv-2.dll",
                "gl" => "opengl32.dll",
                _ => libraryName
            };
        }
        else
        {
            filename = libraryName switch
            {
                "mpv" => "libmpv.so.2",
                "gl" => "libGL.so.1",
                _ => libraryName
            };
        }
        _libraries[libraryName] = NativeLibrary.Load(filename, assembly, searchPath);
        return _libraries[libraryName];
    }
}