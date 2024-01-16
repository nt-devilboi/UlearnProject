using MediatR;
using UlearnTodoTimer.Domen.Entities;
using UlearnTodoTimer.Repositories;

namespace UlearnTodoTimer.Application;

public class HandleGetDataInfoByTodo : IRequestHandler<GetInfoByTaskRequest, InfoTodoResponse>
{
    private readonly ITodoRepo _todos;
    private readonly INoteRepo _notes;
    private readonly ISubTaskRepo _subTasks;

    public HandleGetDataInfoByTodo(ITodoRepo todos, INoteRepo notes, ISubTaskRepo subTasks)
    {
        _todos = todos;
        _notes = notes;
        _subTasks = subTasks;
    }

    public async Task<InfoTodoResponse> Handle(GetInfoByTaskRequest request, CancellationToken cancellationToken)
    {
        var todo = await _todos.Get(request.taskId);
        if (todo == null) return new InfoTodoResponse(new Note[] { }, new SubTodo[] { }); //простите меня за такие костыли, но мне лень
        var notes = _notes.Get()
    }
}

public record GetInfoByTaskRequest(Guid taskId) : IRequest<InfoTodoResponse>;

public record InfoTodoResponse
(Note[] notes,
    SubTodo[] SubTodos); // todo: если не будует лень оберни ответ в класс Result, тогда ответ будет выводится с ошибками нормально