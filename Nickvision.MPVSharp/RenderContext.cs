using Nickvision.MPVSharp.Internal;
using System;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp;

/// <summary>
/// OpenGL render context
/// </summary>
public class RenderContext : MPVRenderContext, IDisposable
{
    private const int GL_DRAW_FRAMEBUFFER_BINDING = 0x8CA6;

    private bool _disposed;
    private readonly nint _clientHandle;
    private Func<string, nint>? _getProcAddressFn;
    private MPVRenderUpdateFn? _callback;
    
    /// <summary>
    /// OpenGL GetProcAddress function, used in <see cref="SetupGL"/>.
    /// </summary>
    /// <remarks>
    /// Most of the time, you don't need to set it, MPVSharp will try to use correct function
    /// depending on the platform. If you need to set it, do this before calling <see cref="SetupGL"/>.
    /// </remarks>
    public Func<string, nint> GetProcAddressFn
    {
        set => _getProcAddressFn = value;
    }

    /// <summary>
    /// Constructs RenderContext
    /// </summary>
    public RenderContext(nint clientHandle)
    {
        _disposed = false;
        _clientHandle = clientHandle;
        _callback = null;
    }

    /// <summary>
    /// Finalizes the RenderContext
    /// </summary>
    ~RenderContext() => Dispose(false);

    /// <summary>
    /// Frees resources used by the RenderContext object
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Frees resources used by the RenderContext object
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing)
        {
            Free();
        }
        _disposed = true;
    }
    
    /// <summary>
    /// Setups OpenGL rendering
    /// </summary>
    /// <param name="callback">Action to be called when a new frame needs to be rendered</param>
    /// <exception cref="ClientException">Thrown if unable to setup GL</exception>
    public void SetupGL(Action? callback)
    {
        MPVOpenGLInitParams glParams;
        if (_getProcAddressFn != null)
        {
            glParams = new MPVOpenGLInitParams()
            {
                GetProcAddrFn = (ctx, name) => _getProcAddressFn(name),
                Param = IntPtr.Zero
            };
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            glParams = new MPVOpenGLInitParams()
            {
                GetProcAddrFn = (ctx, name) => {
                    var res = OpenGLHelpers.WGLGetProcAddress(name);
                    if (res != IntPtr.Zero)
                    {
                        return res;
                    }
                    return OpenGLHelpers.WindowsGetProcAddress(name);
                },
                Param = IntPtr.Zero
            };
        }
        else
        {
            glParams = new MPVOpenGLInitParams()
            {
                GetProcAddrFn = (ctx, name) => OpenGLHelpers.EGLGetProcAddress(name),
                Param = IntPtr.Zero
            };
        }
        var glParamsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(glParams));
        Marshal.StructureToPtr(glParams, glParamsPtr, false);
        var glStringPtr = Marshal.StringToCoTaskMemUTF8("opengl");
        var renderParams = new MPVRenderParam[]
        {
            new MPVRenderParam { Type = MPVRenderParamType.APIType, Data = glStringPtr },
            new MPVRenderParam { Type = MPVRenderParamType.OpenGLInitParams, Data = glParamsPtr },
            new MPVRenderParam { Type = MPVRenderParamType.Invalid, Data = IntPtr.Zero }
        };
        var success = Create(_clientHandle, renderParams);
        Marshal.FreeHGlobal(glParamsPtr);
        Marshal.FreeCoTaskMem(glStringPtr);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
        if (callback != null)
        {
            _callback = (_) => callback();
            SetUpdateCallback(_callback);
        }
    }
    
    /// <summary>
    /// Renders a frame
    /// </summary>
    /// <param name="width">Rendering area width</param>
    /// <param name="height">Rendering area height</param>
    /// <param name="fb">Framebuffer object name</param>
    public void RenderGL(int width, int height, int? fb = null)
    {
        if (fb == null)
        {
            OpenGLHelpers.GLGetIntegerV(GL_DRAW_FRAMEBUFFER_BINDING, out var temp);
            fb = temp;
        }
        var fbo = new MPVOpenGLFBO
        {
            FBO = fb.Value,
            Width = width,
            Height = height
        };
        var fboPtr = Marshal.AllocHGlobal(Marshal.SizeOf(fbo));
        Marshal.StructureToPtr(fbo, fboPtr, false);
        var flipY = 1;
        var flipYPtr = Marshal.AllocHGlobal(Marshal.SizeOf(flipY));
        Marshal.WriteInt32(flipYPtr, flipY);
        var renderParams = new MPVRenderParam []
        {
            new MPVRenderParam { Type = MPVRenderParamType.OpenGLFBO, Data = fboPtr },
            new MPVRenderParam { Type = MPVRenderParamType.FlipY, Data = flipYPtr },
            new MPVRenderParam { Type = MPVRenderParamType.Invalid, Data = IntPtr.Zero }
        };
        var success = Render(renderParams);
        Marshal.FreeHGlobal(fboPtr);
        Marshal.FreeHGlobal(flipYPtr);
        if (success < MPVError.Success)
        {
            throw new ClientException(success);
        }
    }
}