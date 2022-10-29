using System.Diagnostics.CodeAnalysis;

namespace Sasly;

public class SaslException : Exception
{
    private SaslException(string? message) : base(message)
    { }

    [DoesNotReturn]
    internal static void ThrowPropertyNotFound(string methodName, string propName)
    {
        throw new SaslException($"SASL method {methodName} requires property {propName}.");
    }
}
