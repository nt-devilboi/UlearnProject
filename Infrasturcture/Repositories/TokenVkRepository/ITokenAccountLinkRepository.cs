using TgBot.controller.model;
using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public interface ITokenAccountLinkRepository
{
    public Task Add(Token token);
    public Task<Token?> Get(string tokenValue);
    public Task Remove(int id);
}