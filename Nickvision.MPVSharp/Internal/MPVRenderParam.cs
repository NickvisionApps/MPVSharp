using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Used to pass arbitrary parameters to some <see cref="MPVRenderContext"/>'s functions.
/// </summary>
/// <remarks>
/// As a convention, parameter arrays are always terminated by { MPVRenderParamType.Invalid, 0 }
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct MPVRenderParam
{
    public MPVRenderParamType Type;
    public nint Data;
}