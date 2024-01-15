using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public interface ITodoRepo
{
    public Task<Todo?> Get(Guid id);
    public Task Insert(Todo todo);
    public Task<Todo[]> GetAll(Guid tokenId);
    void Update(Todo? todo);
    Task Delete(Guid id);
}