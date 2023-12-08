using System.ComponentModel.DataAnnotations.Schema;

namespace UlearnTodoTimer.Domen.Entities;

[Table("todo", Schema = "ulearn_project")]
public class User
{
    [Column("Id")] public string Id { get; set; }
}