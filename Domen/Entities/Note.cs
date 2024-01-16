using System.ComponentModel.DataAnnotations.Schema;
using UlearnTodoTimer.Controllers.Model;

namespace UlearnTodoTimer.Domen.Entities;

[Table("todo", Schema = "ulearn_project")]
public class Note
{
    [Column("id")] public Guid Id { get; set; }
    [Column("text")] public string? Text { get; set; }
    [Column("task_id")] public Guid TaskId { get; set; }

     public static Note From(NoteRequest noteRequest)
     {
         return new()
         {
             //!!!
             Id = Guid.NewGuid(),
             Text = noteRequest.ToString(),
             TaskId = noteRequest.TodoId
         };
     }
}

public class NoteRequest
{
    public string Text { get; set; }
    public Guid TodoId { get; set; }
}