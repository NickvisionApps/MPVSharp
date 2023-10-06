using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// For <see cref="MPVRenderParamType.DRMDrawSurfaceSize"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVOpenGLDRMDrawSurfaceSize
{
    /// <summary>
    /// Width of the draw plane surface in pixels.
    /// </summary>
    public int Width;
    /// <summary>
    /// Height of the draw plane surface in pixels.
    /// </summary>
    public int Height;
}