using Nickvision.MPVSharp.Internal;
using System;

namespace Nickvision.MPVSharp;

/// <summary>
/// MPV Client Exception
/// </summary>
public class ClientException : Exception
{
    /// <summary>
    /// MPV Error code
    /// </summary>
    public MPVError Error { get; init; }

    /// <summary>
    /// Constructs Client Exception
    /// </summary>
    /// <param name="error">Error code</param>
    public ClientException(MPVError error) : base(error.ToMPVErrorString())
    {
        Error = error;
    }
}