using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public interface ITaskRepo
{
    public Todo Get(Guid id);
    public Task Insert(Todo todo);
}