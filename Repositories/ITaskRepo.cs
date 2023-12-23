using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public interface ITaskRepo
{
    public Task<Todo> Get(Guid id);
    public Task Insert(Todo todo);
    public Task<List<Todo>> Get(User user);
    void Update(Todo todo);
    Task Delete(Guid id);
}