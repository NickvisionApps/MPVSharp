using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Helper functions for OpenGL
/// </summary>
public static partial class OpenGLHelpers
{
    [LibraryImport("libEGL.so.1", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint eglGetProcAddress(string name);
    [LibraryImport("libGL.so.1")]
    private static partial void glGetIntegerv(int pname, out int data);

    /// <summary>
    /// Get the address of EGL function
    /// </summary>
    /// <param name="name">Function name</param>
    /// <returns>Function address</returns>
    public static nint EGLGetProcAddress(string name) => eglGetProcAddress(name);

    /// <summary>
    /// Get the value of selected parameter
    /// </summary>
    /// <param name="pname">Parameter</param>
    /// <param name="data">Result data</param>
    public static void GLGetIntegerV(int pname, out int data) => glGetIntegerv(pname, out data);
}