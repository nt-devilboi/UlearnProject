using TgBot.controller.model;

namespace UlearnTodoTimer.Repositories;

public interface IAppAuth
{
    Task<AccessTokenResponse?> GetAccessToken(string code);
}