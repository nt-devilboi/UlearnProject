using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Infrasturcture.Repositories.SubTodoRepo;

public interface ISubTodoRepo
{
    public Task<SubTodo> Get(Guid id);
    public Task Insert(SubTodo subTodo);
    public Task<List<SubTodo>> GetAll(Guid user);
    void Update(SubTodo? subTodo);
    Task Delete(Guid id);
}