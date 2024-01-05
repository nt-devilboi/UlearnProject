using TgBot.controller.model;
using UlearnTodoTimer.Domen.Entities;
using VkNet.Model;

namespace UlearnTodoTimer.Repositories;

public class TokenAccountLinkRepository: ITokenAccountLinkRepository
{
    public List<Token> Tokens = new List<Token>();
    
    public async Task Add(Token token)
    {
        Tokens.Add(token);
    }

    public async Task<Token?> Get(string tokenValue)
    {
        return Tokens.FirstOrDefault(x => x.Value == tokenValue);
    }

    public Task Remove(int id)
    {
        throw new NotSupportedException();
    }
}