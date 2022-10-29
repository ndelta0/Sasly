using System.Text;

namespace Sasly.Tests;

public class PlainTests
{
    [Theory]
    [InlineData("TestUsername", "TestPassword", "\0TestUsername\0TestPassword")]
    [InlineData("JohnSmith", "Smithy123", "\0JohnSmith\0Smithy123")]
    public void Successful(string username, string password, string expected)
    {
        using var plain = SaslFactory.Create(SaslMechanisms.Plain);

        plain.SetProp(SaslProps.Plain.Username, username);
        plain.SetProp(SaslProps.Plain.Password, password);

        var result = plain.ComputeResponse(Array.Empty<byte>());
        var actual = Encoding.UTF8.GetString(result);

        Assert.True(plain.IsCompleted);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ThrowsNoUsername()
    {
        using var plain = SaslFactory.Create(SaslMechanisms.Plain);

        plain.SetProp(SaslProps.Plain.Password, "Password");

        Assert.Throws<SaslException>(() => plain.ComputeResponse(Array.Empty<byte>()));
    }

    [Fact]
    public void ThrowsNoPassword()
    {
        using var plain = SaslFactory.Create(SaslMechanisms.Plain);

        plain.SetProp(SaslProps.Plain.Username, "Username");

        Assert.Throws<SaslException>(() => plain.ComputeResponse(Array.Empty<byte>()));
    }

    [Fact]
    public void ThrowsInvalidUsernameType()
    {
        using var plain = SaslFactory.Create(SaslMechanisms.Plain);

        plain.SetProp(SaslProps.Plain.Username, 0);
        plain.SetProp(SaslProps.Plain.Password, "Password");

        Assert.Throws<InvalidCastException>(() => plain.ComputeResponse(Array.Empty<byte>()));
    }

    [Fact]
    public void ThrowsInvalidPasswordType()
    {
        using var plain = SaslFactory.Create(SaslMechanisms.Plain);

        plain.SetProp(SaslProps.Plain.Username, "Username");
        plain.SetProp(SaslProps.Plain.Password, 0);

        Assert.Throws<InvalidCastException>(() => plain.ComputeResponse(Array.Empty<byte>()));
    }
}
