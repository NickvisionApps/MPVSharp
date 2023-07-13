using Nickvision.MPVSharp.Internal;

namespace Nickvision.MPVSharp;

public class ClientException : Exception
{
    public ClientException(MPVError error) : base(error.ToMPVErrorString())
    {
    }
}