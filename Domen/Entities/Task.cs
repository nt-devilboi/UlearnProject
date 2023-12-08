using System.ComponentModel.DataAnnotations.Schema;
using UlearnTodoTimer.Controllers.Model;

namespace UlearnTodoTimer.Domen.Entities;

[Table("todo", Schema = "ulearn_project")]
public class Todo
{
    [Column("id")] public Guid Id { get; set; }
    [Column("is_complete")]  public bool IsComplete { get; set; }
    [Column("user_id")] public string UserId { get; set; } 
    [Column("title")] public string Title { get; set; }
    [Column("desc")] public string Desc { get; set; }
    [Column("time_start")] public DateTime TimeStart { get; set; }
    [Column("time_end")] public DateTime TimeEnd { get; set; }
    
    public static Todo From(TodoRequest todoRequest, User user)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            IsComplete = false,
            Desc = todoRequest.Desc,
            Title = todoRequest.Title,
            TimeEnd = todoRequest.TimeEnd,
            TimeStart = todoRequest.TimeStart
        };
    }
}