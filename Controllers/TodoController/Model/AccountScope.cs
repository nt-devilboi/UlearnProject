using UlearnTodoTimer.Domen.Entities;
using UlearnTodoTimer.Infrasturcture;


namespace UlearnTodoTimer.Controllers.Model;

public class UserInfoScope
{
    public UserInfo UserInfo { get; set; }
    public TokenInfo Token  { get; set; }
}