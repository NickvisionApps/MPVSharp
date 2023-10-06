using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// For <see cref="MPVRenderParamType.DRMDisplayV2"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MPVOpenGLDRMParamsV2
{
    /// <summary>
    /// DRM fd (int). Set to -1 if invalid.
    /// </summary>
    public int Fd;
    /// <summary>
    /// Currently used Crtc Id
    /// </summary>
    public int CrtcId;
    /// <summary>
    /// Currently used connector id
    /// </summary>
    public int ConnectorId;
    /// <summary>
    /// Pointer to a drmModeAtomicReq pointer that is being used for the renderloop.
    /// This pointer should hold a pointer to the atomic request pointer
    /// The atomic request pointer is usually changed at every renderloop.
    /// </summary>
    public nint AtomicRequestPtr;
    /// <summary>
    /// DRM render node. Used for VAAPI interop.
    /// Set to -1 if invalid.
    /// </summary>
    public int RenderFd;
}