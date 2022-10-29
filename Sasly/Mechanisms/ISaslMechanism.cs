namespace Sasly;

public interface ISaslMechanism : IDisposable
{
    public static string Name => "";

    public bool IsCompleted { get; }

    public void SetProp(string name, object value);
    public bool HasProp(string name);
    public object GetProp(string name);

    public byte[] ComputeResponse(byte[] challenge);

    public string ComputeResponseBase64(string challenge)
    {
        var input = Convert.FromBase64String(challenge);
        var output = ComputeResponse(input);
        return Convert.ToBase64String(output);
    }
}
