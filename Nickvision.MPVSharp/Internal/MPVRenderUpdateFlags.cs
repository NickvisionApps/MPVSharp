namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Flags returned by MPVRenderContext.Update()
/// </summary>
public enum MPVRenderUpdateFlags
{
    /// <summary>
    /// A new video frame must be rendered. MPVRenderContext.Render() must be called.
    /// </summary>
    UpdateFrame = 1 << 0
}