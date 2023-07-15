using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

[StructLayout(LayoutKind.Sequential)]
public struct MPVOpenGLInitParams
{
    public GetProcAddr GetProcAddrFn;
    public nint Param;
    
    public delegate nint GetProcAddr(nint ctx, string name);
}