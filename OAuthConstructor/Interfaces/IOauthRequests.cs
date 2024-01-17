namespace UlearnTodoTimer.OAuthConstructor.Interfaces;

public interface IOauthRequests
{
    public string CreateAuthRequest(string state);
    public string CreateGetAccessTokenRequest(string code);
    
    
}