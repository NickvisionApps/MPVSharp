using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// For initializing the mpv OpenGL state via <see cref="MPVRenderParamType.OpenGLInitParams"/>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVOpenGLInitParams
{
    /// <summary>
    /// This retrieves OpenGL function pointers, and will use them in subsequent operation.
    /// </summary>
    public GetProcAddr GetProcAddrFn;
    /// <summary>
    /// Value passed as ctx parameter to GetProcAddrFn.
    /// </summary>
    public nint Param;
    
    public delegate nint GetProcAddr(nint ctx, string name);
}