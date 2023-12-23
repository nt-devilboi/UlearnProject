namespace UlearnTodoTimer.Controllers.Model;

public class TodoRequest
{
    public bool IsComplete;
    public string Title { get; set; }
    public string Desc { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
}
