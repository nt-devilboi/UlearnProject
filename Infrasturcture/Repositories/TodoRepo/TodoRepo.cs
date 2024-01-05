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

    public async Task<Todo[]> GetAll(Guid tokenId)
    {
        return _todos.Where(x => x.TokenId == tokenId).ToArray();
    }
    
    public void Update(Todo? todo)
    {
        var x = _todos.FirstOrDefault(x => x.Id == todo.Id);
        if (x != null)
        {
            _todos.Remove(x);
            _todos.Add(todo);
        }
    }

    public async Task Delete(Guid id)
    {
        var x = _todos.FirstOrDefault(x => x.Id == id);
        if (x != null)
        {
            _todos.Remove(x);
        }
        
    }
}