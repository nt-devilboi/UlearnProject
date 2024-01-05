namespace UlearnTodoTimer;

public interface ISubTaskRepo
{
    public SubTask<Todo> Get(Guid id);
    public SubTask Insert(Todo todo);
    public SubTask<List<Todo>> Get(User user);
    void Update(Todo todo);
    SubTask Delete(Guid id);
}