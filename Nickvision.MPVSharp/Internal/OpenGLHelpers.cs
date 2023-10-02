using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Helper functions for OpenGL
/// </summary>
public static partial class OpenGLHelpers
{
    [LibraryImport("libEGL.so.1", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint eglGetProcAddress(string name);
    [LibraryImport("opengl32.dll", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint wglGetProcAddress(string name);
    [LibraryImport("kernel32.dll", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint GetProcAddress(nint module, string name);
    [LibraryImport("gl")]
    private static partial void glGetIntegerv(int pname, out int data);

    private static nint? _openGLDllPtr;
    
    /// <summary>
    /// Gets the address of EGL function
    /// </summary>
    /// <param name="name">Function name</param>
    /// <returns>Function address</returns>
    public static nint EGLGetProcAddress(string name) => eglGetProcAddress(name);

    /// <summary>
    /// Gets the address of WGL function
    /// </summary>
    /// <param name="name">Function name</param>
    /// <returns>Function address</returns>
    public static nint WGLGetProcAddress(string name) => wglGetProcAddress(name);

    /// <summary>
    /// Gets the address of function on Windows
    /// </summary>
    /// <param name="name">Function name</param>
    /// <returns>Function address</returns>
    /// <remarks>WGLGetProcAddress returns 0 for some OpenGL functions
    /// (for example, glGetString), this method is a fallback, details can be found at
    /// <a href="https://www.khronos.org/opengl/wiki/Load_OpenGL_Functions#Windows">
    /// OpenGL documentation</a></remarks>
    public static nint WindowsGetProcAddress(string name)
    {
        _openGLDllPtr ??= NativeLibrary.Load("opengl32.dll");
        return GetProcAddress(_openGLDllPtr.Value, name);
    }

    /// <summary>
    /// Gets the value of selected parameter
    /// </summary>
    /// <param name="pname">Parameter</param>
    /// <param name="data">Result data</param>
    public static void GLGetIntegerV(int pname, out int data) => glGetIntegerv(pname, out data);
}