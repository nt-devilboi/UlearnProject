using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public class TodoRepoFake : ITodoRepo
{
    private readonly List<Todo> _todos;

    public TodoRepoFake()
    {
        _todos = new List<Todo>();
    }

    public Task<Todo?> Get(Guid id)
    {
        return Task.FromResult(_todos.FirstOrDefault(x => x.Id == id));
    }

    public Task Insert(Todo todo)
    {
        _todos.Add(todo);
        return Task.CompletedTask;
    }

    public Task<List<Todo>> GetAll(Guid tokenId)
    {
        throw new NotImplementedException();
    }
    
    public void Update(Todo? todo)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}