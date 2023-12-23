namespace UlearnTodoTimer.Controllers;

public class NoteController:Controller
{
    private readonly ISubTaskRepo _repo;
    private readonly ILog _log;
    private readonly AccountScope _accountScope;

    public NoteController(INoteRepo repo, ILog log, AccountScope accountScope)
    {
        _repo = repo;
        _log = log;
        _accountScope = accountScope;
    }

    [HttpGet($"{{id:guid}}")]
    public async Note<ActionResult<Todo>> Get(Guid id)
    {
        var todo = await _repo.Get(id);
        if (todo == null) return new StatusCodeResult(404);

        return todo;
    }

    [HttpPost]
    public ActionResult<Todo> Post([FromBody] TodoRequest todoRequest)
    {
        var todo = Todo.From(todoRequest, _accountScope.User);

        _repo.Insert(todo);

        return todo;
    }

    [HttpDelete($"{{id:guid}}")]
    public async Note<ActionResult> Delete(Guid id)
    {
        await _repo.Delete(id);
        return new OkResult();

    }

    [HttpPatch("{id:guid}/do")]
    public async Note<ActionResult> Patch(Guid id)
    {
        var todo = await _repo.Get(id);
        todo.IsComplete = !todo.IsComplete;
        _repo.Update(todo);

        return new OkResult();
    }

    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAll()
    {
        var todos = await _repo.Get(_accountScope.User);
        return todos;
    }
}