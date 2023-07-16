using Nickvision.MPVSharp.Internal;
using System.Runtime.InteropServices;

namespace Nickvision.MPVSharp;

public class RenderContext : MPVRenderContext, IDisposable
{
    private const int GLDrawFramebufferBinding = 0x8CA6;
    private readonly nint _clientHandle;
    private bool _disposed;
    private MPVRenderUpdateFn? _callback;
    
    public RenderContext(nint clientHandle)
    {
        _disposed = false;
        _clientHandle = clientHandle;
    }
    
    public void SetupGL(MPVRenderUpdateFn? callback)
    {
        var glParams = new MPVOpenGLInitParams
        {
            GetProcAddrFn = (ctx, name) => OpenGLHelpers.EGLGetProcAddress(name),
            Param = IntPtr.Zero
        };
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
            _callback = callback;
            SetUpdateCallback(_callback);
        }
    }
    
    public void RenderGL(int width, int height)
    {
        int fboInt;
        OpenGLHelpers.GLGetIntegerV(GLDrawFramebufferBinding, out fboInt);
        var fbo = new MPVOpenGLFBO
        {
            FBO = fboInt,
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
}