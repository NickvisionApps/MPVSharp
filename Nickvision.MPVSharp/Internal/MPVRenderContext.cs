using System;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// MPV Render Context. This object can be used to make mpv render using supported graphic APIs (such as OpenGL).
/// </summary>
public partial class MPVRenderContext
{
    public delegate void MPVRenderUpdateFn(nint data);

    [LibraryImport("mpv")]
    private static partial MPVError mpv_render_context_create(out nint ctx, nint handle, MPVRenderParam[] param);
    [LibraryImport("mpv")]
    private static partial MPVError mpv_render_context_set_parameter(nint ctx, MPVRenderParam param);
    [LibraryImport("mpv")]
    private static partial MPVError mpv_render_context_get_info(nint ctx, MPVRenderParam param);
    [LibraryImport("mpv")]
    private static partial void mpv_render_context_set_update_callback(nint ctx, MPVRenderUpdateFn callback, nint data);
    [LibraryImport("mpv")]
    private static partial ulong mpv_render_context_update(nint ctx);
    [LibraryImport("mpv")]
    private static partial MPVError mpv_render_context_render(nint ctx, MPVRenderParam[] param);
    [LibraryImport("mpv")]
    private static partial void mpv_render_context_report_swap(nint ctx);
    [LibraryImport("mpv")]
    private static partial void mpv_render_context_free(nint ctx);
    
    private nint _handle;
    
    /// <summary>
    /// Constructs MPVRenderContext
    /// </summary>
    public MPVRenderContext()
    {
        _handle = IntPtr.Zero;
    }
    
    /// <summary>
    /// Initializes the renderer state. Depending on the backend used, this will access the underlying GPU API and initialize its own objects.
    /// </summary>
    /// <param name="clientHandle">Pointer to <see cref="MPVClient"/></param>
    /// <param name="param">An array of parameters, terminated by { MPVRenderParamType.Invalid, 0 }</param>
    /// <returns>Error code</returns>
    public MPVError Create(nint clientHandle, MPVRenderParam[] param) => mpv_render_context_create(out _handle, clientHandle, param);
    
    /// <summary>
    /// Attempts to change a single parameter. Not all backends and parameter types support all kinds of changes.
    /// </summary>
    /// <param name="param">The parameter type and data that should be set</param>
    /// <returns>Error code</returns>
    public MPVError SetParameter(MPVRenderParam param) => mpv_render_context_set_parameter(_handle, param);
    
    /// <summary>
    /// Retrieves information from the render context.
    /// </summary>
    /// <param name="param">The parameter type and data that should be retrieved</param>
    /// <returns>Error code</returns>
    public MPVError GetInfo(MPVRenderParam param) => mpv_render_context_get_info(_handle, param);
    
    /// <summary>
    /// Sets the callback that notifies you when a new video frame is available, or if the video display configuration somehow changed and requires a redraw.
    /// </summary>
    /// <remarks>
    /// You must not call any mpv API from the callback or exit from callback in unusual way (such as by throwing an exception)
    /// </remarks>
    /// <param name="callback">Callback that will be called if the frame should be redrawn</param>
    /// <param name="data">Arbitrary data pointer to pass to the callback</param>
    public void SetUpdateCallback(MPVRenderUpdateFn callback, nint data = 0) => mpv_render_context_set_update_callback(_handle, callback, data);
    
    /// <summary>
    /// The API user is supposed to call this when the update callback was invoked
    /// </summary>
    /// <remarks>
    /// This has to happen on the render thread, and _not_ from the update callback itself.
    /// This is optional if MPVRenderParam with type AdvancedControl wasn't set.
    /// </remarks>
    /// <returns>A bitset of MPVRenderUpdateFlag values indicating what should happen next</returns>
    public ulong Update() => mpv_render_context_update(_handle);

    /// <summary>
    /// Renders video.
    /// </summary>
    /// <param name="param">An array of parameters, terminated by { MPVRenderParamType.Invalid, 0 }</param>
    /// <returns>Error code</returns>
    /// <remarks>
    /// You should pass the following parameters:
    /// - Backend-specific target object, such as <see cref="MPVRenderParamType.OpenGLFBO"/>.
    /// - Possibly transformations, such as <see cref="MPVRenderParamType.FlipY"/>.
    /// </remarks>
    public MPVError Render(MPVRenderParam[] param) => mpv_render_context_render(_handle, param);

    /// <summary>
    /// Tells the renderer that a frame was flipped at the given time. This is optional, but can help the player to achieve better timing.
    /// </summary>
    /// <remarks>
    /// Calling this at least once informs libmpv that you will use this function. If you use it inconsistently, expect bad video playback.
    /// </remarks>
    public void ReportSwap() => mpv_render_context_report_swap(_handle);

    /// <summary>
    /// Destroys the mpv renderer state.
    /// </summary>
    public void Free() => mpv_render_context_free(_handle);
}