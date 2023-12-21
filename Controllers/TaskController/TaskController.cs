using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.Domen.Entities;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.Controllers;


//todo: код написан упрощенно, ибо мне пока лень думать, но концептуально все именно так. как минимум нету проверок ваще. если не понятно. либо спросить меня, либо phind.com`
[ApiController]
[Route("api/tasks")]
public class TaskController
{
    private readonly ITaskRepo _taskRepo;
    private readonly ILog _log;
    private readonly TokenScope _tokenScope;
    
    public TaskController(ITaskRepo taskRepo, ILog log, TokenScope tokenScope)
    {
        _taskRepo = taskRepo;
        _log = log;
        _tokenScope = tokenScope;
    }

    [HttpGet($"{{id:guid}}")]
    public async Task<ActionResult<Todo>> Get(Guid id)
    {
        var todo = await _taskRepo.Get(id);
        if (todo == null) return new StatusCodeResult(404);

        if (todo.UserId != _tokenScope.Token.Value) return new StatusCodeResult(403);
        
        
        return todo;
    }

    [HttpPost]
    public ActionResult<Todo> Post([FromBody] TodoRequest todoRequest)
    {
        var todo = Todo.From(todoRequest, _tokenScope.Token);
        
        _taskRepo.Insert(todo);

        return todo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAll()
    {
        var todos = await _taskRepo.Get(_tokenScope.Token);

        return todos;
    }

    [HttpDelete($"{{id:guid}}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _taskRepo.Delete(id);

        return new OkResult();
    }

    [HttpPatch("{id:guid}/do")]
    public async Task<ActionResult> Patch(Guid id)
    {
        var todo = await _taskRepo.Get(id);
        
        todo.IsComplete = !todo.IsComplete;

        _taskRepo.Update(todo);

        return new OkResult();
    }
}