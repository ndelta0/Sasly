using System.Text;

namespace Sasly;

public class SaslPlain : ISaslMechanism
{
    public string Name => "PLAIN";

    public bool IsCompleted { get; private set; }

    private readonly Dictionary<string, object> _props = new();
    public void SetProp(string name, object value) => _props[name] = value;
    public bool HasProp(string name) => _props.ContainsKey(name);
    public object GetProp(string name) => _props[name];

    public byte[] ComputeResponse(byte[] challenge)
    {
        if (!HasProp(SaslProps.Plain.Username))
        {
            SaslException.ThrowPropertyNotFound(Name, SaslProps.Plain.Username);
        }

        if (!HasProp(SaslProps.Plain.Password))
        {
            SaslException.ThrowPropertyNotFound(Name, SaslProps.Plain.Password);
        }

        var username = (string)GetProp(SaslProps.Plain.Username);
        var password = (string)GetProp(SaslProps.Plain.Password);

        IsCompleted = true;
        return Encoding.UTF8.GetBytes($"\0{username}\0{password}");
    }
}
