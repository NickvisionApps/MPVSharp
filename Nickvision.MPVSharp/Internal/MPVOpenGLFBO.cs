using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

[StructLayout(LayoutKind.Sequential)]
public struct MPVOpenGLFBO
{
    public int FBO;
    public int Width;
    public int Height;
    public int Format;
}