using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// For <see cref="MPVRenderParamType.OpenGLFBO"/>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVOpenGLFBO
{
    /// <summary>
    /// Framebuffer object name.
    /// </summary>
    public int FBO;
    /// <summary>
    /// Valid width. This must refer to the size of the framebuffer.
    /// </summary>
    public int Width;
    /// <summary>
    /// Valid height. This must refer to the size of the framebuffer.
    /// </summary>
    public int Height;
    /// <summary>
    /// Underlying texture internal format (e.g. GL_RGBA8), or 0 if unknown.
    /// </summary>
    public int Format;
}