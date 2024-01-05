using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Repositories;

public interface INoteRepo
{
    public Task<Note?> Get(Guid id);
    public Note Insert(Note todo);
    public Task<List<Note>> GetAll (Guid taskId);
    void Update(Note note);
    Task Delete(Guid id);
}