using System.ComponentModel.DataAnnotations.Schema;
using UlearnTodoTimer.Controllers.Model;

namespace UlearnTodoTimer.Domen.Entities;

[Table("todo", Schema = "ulearn_project")]
public class Todo
{
    [Column("is_complete")]  private bool IsComplete { get; set; }


    public static Todo From(TodoRequest todoRequest)
    {
        
    }
}