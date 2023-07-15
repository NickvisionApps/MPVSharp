using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp.Internal;

public partial class MPVRenderContext
{
    [LibraryImport("libmpv.so.2")]
    private static partial MPVError mpv_render_context_create(out nint ctx, nint handle, MPVRenderParam[] param);
    [LibraryImport("libmpv.so.2")]
    private static partial MPVError mpv_render_context_set_parameter(nint ctx, MPVRenderParam param);
    [LibraryImport("libmpv.so.2")]
    private static partial MPVError mpv_render_context_get_info(nint ctx, MPVRenderParam param);
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_render_context_set_update_callback(nint ctx, MPVRenderUpdateFn callback, nint data);
    [LibraryImport("libmpv.so.2")]
    private static partial ulong mpv_render_context_update(nint ctx);
    [LibraryImport("libmpv.so.2")]
    private static partial MPVError mpv_render_context_render(nint ctx, MPVRenderParam[] param);
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_render_context_report_swap(nint ctx);
    [LibraryImport("libmpv.so.2")]
    private static partial void mpv_render_context_free(nint ctx);
    [LibraryImport("libEGL.so.1", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint eglGetProcAddress(string name);
    [LibraryImport("libGL.so.1")]
    private static partial void glGetIntegerv(int pname, out int data);

    public delegate void MPVRenderUpdateFn(nint data);
    
    private nint _handle;
    
    public MPVRenderContext()
    {
        _handle = IntPtr.Zero;
    }
    
    public MPVError Create(nint clientHandle, MPVRenderParam[] param) => mpv_render_context_create(out _handle, clientHandle, param);
    
    public MPVError SetParameter(MPVRenderParam param) => mpv_render_context_set_parameter(_handle, param);
    
    public MPVError GetInfo(MPVRenderParam param) => mpv_render_context_get_info(_handle, param);
    
    public void SetUpdateCallback(MPVRenderUpdateFn callback, nint data = 0) => mpv_render_context_set_update_callback(_handle, callback, data);
    
    public ulong Update() => mpv_render_context_update(_handle);

    public MPVError Render(MPVRenderParam[] param) => mpv_render_context_render(_handle, param);

    public void ReportSwap() => mpv_render_context_report_swap(_handle);

    public void Free() => mpv_render_context_free(_handle);
    
    public nint EglGetProcAddress(string name) => eglGetProcAddress(name);
    
    public void GLGetIntegerv(int pname, out int data) => glGetIntegerv(pname, out data);
}