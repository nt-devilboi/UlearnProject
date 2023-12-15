using System.ComponentModel.DataAnnotations.Schema;
using UlearnTodoTimer.Controllers.Model;

namespace UlearnTodoTimer.Domen.Entities;

[Table("todo", Schema = "ulearn_project")]
public class SubTask
{
    [Column("id")] public Guid Id { get; set; }
    [Column("Id_task")] public string TaskId { get; set; }
    [Column("is_complete")] public boolean IsComplete { get; set; }
    [Column("title")] public string Title { get; set; }

}
