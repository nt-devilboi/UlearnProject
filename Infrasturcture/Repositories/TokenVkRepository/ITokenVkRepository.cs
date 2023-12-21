using TgBot.controller.model;
using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public interface ITokenAccountLinkRepository
{
    public Task Add(AccessToken tokenResponse);
    public Task<Token?> Get(string token);
    public Task Remove(int id);
}