using System.ComponentModel.DataAnnotations.Schema;

namespace UlearnTodoTimer.Domen.Entities;

[Table("todo", Schema = "ulearn_project")]
public class Token
{
    [Column("id")] public string Id { get; set; }
    [Column("value")] public string Value { get; set; }
}