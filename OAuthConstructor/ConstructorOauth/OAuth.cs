
using UlearnTodoTimer.OAuthConstructor.Interfaces;

namespace UlearnTodoTimer.FluetApi.ConstructorOauth;


// привет принцип ISP (разделение интерфейсов)
public class OAuths : IRegisterOAuth, IProvideOAuth
{
    private readonly Dictionary<string, IOauthRequests> OauthRequestsMap = new Dictionary<string, IOauthRequests>();
    public IReadOnlyDictionary<string, IOauthRequests> GetAll => OauthRequestsMap;
    
    public OAuths AddOAuth(string name, Action<ConstructorOAuth> ConfigureOAuth)
    {
        if (name == string.Empty)
        {
            throw new ArgumentException("name is empty");
        }

        if (ConfigureOAuth == null)
        {
            throw new ArgumentNullException(nameof(ConfigureOAuth));
        }

        var ctorOAuth = new ConstructorOAuth();
        ConfigureOAuth(ctorOAuth);
        OauthRequestsMap.Add(name, ctorOAuth.Build()); // todo: реализовать проверки на то, что запрос может сущестовать и работать.

        return this;
    }

    public IOauthRequests GetOAuth(string name)
    {
        if (!OauthRequestsMap.TryGetValue(name, out var value))
        {
            throw new ArgumentException();
        }

        return value;
    }

   

    public static IRegisterOAuth CreateBuilder()
    {
        return new OAuths();
    }
}