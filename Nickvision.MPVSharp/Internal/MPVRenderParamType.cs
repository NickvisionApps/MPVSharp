namespace Nickvision.MPVSharp.Internal;

public enum MPVRenderParamType
{
    Invalid = 0,
    ApiType,
    OpenGLInitParams,
    OpenGLFBO,
    FlipY,
    Depth,
    ICCProfile,
    AmbientLight,
    X11Display,
    WLDisplay,
    AdvancedControl,
    NextFrameInfo,
    BlockForTargetTime,
    SkipRendering,
    DRMDrawSurfaceSize = 15,
    DRMDisplayV2,
    SWSize,
    SWFormat,
    SWStride,
    SWPointer
}