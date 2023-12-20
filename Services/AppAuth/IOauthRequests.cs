namespace UlearnTodoTimer.Services.AppAuth;

public interface IOauthRequests
{
    public string CreateAuthRequest(long state);
    public string CreateGetAccessTokenRequest(string code);
}