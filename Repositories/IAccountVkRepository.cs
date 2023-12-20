using TgBot.controller.model;
using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public interface ITokenAccountVkRepository
{
    public Task Add(AccessToken tokenResponse, long chatId);
    public Task<Token?> Get(string token);
    public Task Remove(int id);
}