using TgBot.controller.model;

namespace UlearnTodoTimer.OAuthConstructor;

public interface IOAuthService
{
    public Task<string?> GetAccessToken(string state, string code);

    public List<string> CreateOAuthRequests(string state = "");
}