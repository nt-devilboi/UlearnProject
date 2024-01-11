using System.ComponentModel.DataAnnotations.Schema;
using TgBot.controller.model;

namespace UlearnTodoTimer.Domen.Entities;

[Table("token", Schema = "ulearn_project")]
public class Token
{
    [Column("id")] public Guid Id { get; set; }
    [Column("value")] public string Value { get; set; }
    [Column("Authorization_server")] public string AuthorizationServer { get; set; }


    public static Token From(string accessTokenResponse, string authServer)
    {
        return new Token()
        {
            AuthorizationServer = authServer,
            Id = Guid.NewGuid(),
            Value = accessTokenResponse
        };
    }
}