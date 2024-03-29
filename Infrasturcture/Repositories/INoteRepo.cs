using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public interface INoteRepo
{
    public Task<Note?> Get(Guid id);
    public Task Insert(Note note);
    /*public Task<List<Note>> GetAll (Guid taskId);*/
    void Update(Note note);
    Task Delete(Guid id);
}