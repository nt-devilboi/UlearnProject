using UlearnTodoTimer.FluetApi.ConstructorOauth;

namespace UlearnTodoTimer.OAuthConstructor.Interfaces;

public interface IRegisterOAuth
{
    public OAuths AddOAuth(string name, Action<ConstructorOAuth> ConfigureOAuth);
}
