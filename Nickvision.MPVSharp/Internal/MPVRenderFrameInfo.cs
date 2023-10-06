namespace Nickvision.MPVSharp.Internal;

/// <summary>
/// Information about the next video frame that will be rendered. Can be
/// retrieved with <see cref="MPVRenderParamType.NextFrameInfo"/>.
/// </summary>
public struct MPVRenderFrameInfo
{
    /// <summary>
    /// A bitset of <see cref="MPVRenderFrameInfoFlag"/> values (i.e. multiple flags are
    /// combined with bitwise or).
    /// </summary>
    public ulong Flags;
    /// <summary>
    /// Absolute time at which the frame is supposed to be displayed. This is in
    /// the same unit and base as the time returned by <see cref="MPVClient.GetTimeUs"/>. For
    /// frames that are redrawn, or if vsync locked video timing is used (see
    /// "video-sync" option), then this can be 0. The "video-timing-offset"
    /// option determines how much "headroom" the render thread gets (but a high
    /// enough frame rate can reduce it anyway). <see cref="MPVRenderContext.Render"/> will
    /// normally block until the time is elapsed, unless you pass it
    /// <see cref="MPVRenderParamType.BlockForTargetTime"/> = 0.
    /// </summary>
    public ulong TargetTime;
}