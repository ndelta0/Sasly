using System.Linq.Expressions;
using Sasly.Mechanisms;

namespace Sasly;

public static class SaslFactory
{
    private static readonly Dictionary<string, Func<ISaslMechanism>> Methods;

    static SaslFactory()
    {
        Methods = new Dictionary<string, Func<ISaslMechanism>>
        {
            [SaslPlain.Name] = Expression.Lambda<Func<SaslPlain>>(Expression.New(typeof(SaslPlain))).Compile()
        };
    }

    public static ISaslMechanism Create(string name) => Methods[name]();
}
