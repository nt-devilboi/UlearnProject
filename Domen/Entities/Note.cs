using System.ComponentModel.DataAnnotations.Schema;
using UlearnTodoTimer.Controllers.Model;

namespace UlearnTodoTimer.Domen.Entities;

[Table("todo", Schema = "ulearn_project")]
public class Note
{
    [Column("id")] public Guid Id { get; set; }
    [Column("user_id")] public string UserId { get; set; }
    [Column("text")] public string Text { get; set; }
    [Column("task_id")] public string TaskId { get; set; }

    // public static object From(TodoRequest todoRequest, object user)
    // {
    //     return 
    // }
}