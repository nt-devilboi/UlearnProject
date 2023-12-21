using TgBot.controller.model;
using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public class TokenAccountLinkRepostitory: ITokenAccountLinkRepository
{
    public Task Add(AccessToken tokenResponse)
    {
        throw new NotImplementedException();
    }

    public Task<Token?> Get(string token)
    {
        throw new NotImplementedException();
    }

    public Task Remove(int id)
    {
        throw new NotImplementedException();
    }
}