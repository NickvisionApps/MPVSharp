using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

[StructLayout(LayoutKind.Sequential)]
public struct MPVRenderParam
{
    public MPVRenderParamType Type;
    public nint Data;
}