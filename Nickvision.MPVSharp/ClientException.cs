using Nickvision.MPVSharp.Internal;

namespace Nickvision.MPVSharp;

/// <summary>
/// MPV Client Exception
/// </summary>
public class ClientException : Exception
{
    /// <summary>
    /// Construct Client Exception
    /// </summary>
    /// <param name="error">MPVError</param>
    public ClientException(MPVError error) : base(error.ToMPVErrorString())
    {
    }
}