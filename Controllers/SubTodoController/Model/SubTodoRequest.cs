namespace UlearnTodoTimer.Controllers.Model
{
    public class SubTodoRequest
    {
        public bool IsCompleted { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }
        public Guid TodoId { get; set; }
    }
}
