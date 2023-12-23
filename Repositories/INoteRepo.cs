using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public interface INoteRepo
{
    public Note<Todo> Get(Guid id);
    public Note Insert(Todo todo);
    public Note<List<Todo>> Get(User user);
    void Update(Todo todo);
    Note Delete(Guid id);
}