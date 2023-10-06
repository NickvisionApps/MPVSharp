namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Parameters for MPVRenderParam
/// </summary>
public enum MPVRenderParamType
{
    /// <summary>
    /// Not a valid value, but also used to terminate a params array.
    /// </summary>
    Invalid = 0,
    /// <summary>
    /// The render API to use.
    /// </summary>
    /// <remarks>
    /// Type: string
    /// </remarks>
    APIType,
    /// <summary>
    /// Required parameters for initializing the OpenGL renderer.
    /// </summary>
    /// <remarks>
    /// Type: <see cref="MPVOpenGLInitParams"/>
    /// </remarks>
    OpenGLInitParams,
    /// <summary>
    /// Describes a GL render target.
    /// </summary>
    /// <remarks>
    /// Type: <see cref="MPVOpenGLFBO"/>
    /// </remarks>
    OpenGLFBO,
    /// <summary>
    /// Control flipped rendering.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to int
    /// </remarks>
    FlipY,
    /// <summary>
    /// Control surface depth.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to int
    /// </remarks>
    Depth,
    /// <summary>
    /// ICC profile blob
    /// </summary>
    /// <remarks>
    /// Type: Pointer to <see cref="MPVByteArray"/>
    /// </remarks>
    ICCProfile,
    /// <summary>
    /// Ambient light in lux.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to int
    /// </remarks>
    AmbientLight,
    /// <summary>
    /// X11 Display, sometimes used for hwdec.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to display
    /// </remarks>
    X11Display,
    /// <summary>
    /// Wayland display, sometimes used for hwdec.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to struct wl_display
    /// </remarks>
    WLDisplay,
    /// <summary>
    /// Better control about rendering and enabling some advanced features.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to int: 0 for disabled (default), 1 for enabled
    /// </remarks>
    AdvancedControl,
    /// <summary>
    /// Return information about the next frame to render.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to <see cref="MPVRenderFrameInfo"/>
    /// </remarks>
    NextFrameInfo,
    /// <summary>
    /// Enable or disable video timing.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to int: 0 for disabled, 1 for enabled (default)
    /// </remarks>
    BlockForTargetTime,
    /// <summary>
    /// Use to skip rendering.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to int: 0 for rendering (default), 1 for skipping
    /// </remarks>
    SkipRendering,
    /// <summary>
    /// DRM draw surface size, contains draw surface dimensions.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to <see cref="MPVOpenGLDRMDrawSurfaceSize"/>
    /// </remarks>
    DRMDrawSurfaceSize = 15,
    /// <summary>
    /// DRM display, contains drm display handles.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to <see cref="MPVOpenGLDRMParamsV2"/>
    /// </remarks>
    DRMDisplayV2,
    /// <summary>
    /// Rendering target surface size for software rendenring.
    /// </summary>
    /// <remarks>
    /// Type: int[2]
    /// </remarks>
    SWSize,
    /// <summary>
    /// Rendering target surface pixel format for software rendering.
    /// </summary>
    /// <remarks>
    /// Type: string
    /// </remarks>
    SWFormat,
    /// <summary>
    /// Rendering target surface bytes per line for software rendering.
    /// </summary>
    /// <remarks>
    /// Type: Pointer to uint
    /// </remarks>
    SWStride,
    /// <summary>
    /// Rendering target surface pixel data pointer for software rendering.
    /// </summary>
    /// <remarks>
    /// Type: Pointer
    /// </remarks>
    SWPointer
}