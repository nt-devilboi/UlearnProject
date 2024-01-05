using System.ComponentModel.DataAnnotations.Schema;
using UlearnTodoTimer.Controllers.Model;

namespace UlearnTodoTimer.Domen.Entities;

[Table("subTodo", Schema = "ulearn_project")]
public class SubTodo
{
    [Column("id")] public Guid Id { get; set; }
    [Column("Id_task")] public string TaskId { get; set; }
    [Column("is_complete")] public bool IsComplete { get; set; }
    [Column("title")] public string Title { get; set; }

    public static SubTodo From(SubTodoRequest subTodoRequest, Guid tokenId)
    {
        return new SubTodo 
        { 
            Id = Guid.NewGuid(), 
            Title = subTodoRequest.Title, 
            IsComplete = false
        };
    }
}
