using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

public static partial class OpenGLHelpers
{
    [LibraryImport("libEGL.so.1", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint eglGetProcAddress(string name);
    [LibraryImport("libGL.so.1")]
    private static partial void glGetIntegerv(int pname, out int data);

    public static nint EglGetProcAddress(string name) => eglGetProcAddress(name);

    public static void GLGetIntegerv(int pname, out int data) => glGetIntegerv(pname, out data);
}