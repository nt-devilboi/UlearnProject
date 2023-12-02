using Microsoft.AspNetCore.Mvc;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.Domen.Entities;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.Controllers;

[ApiController]
[Route("api/task")]
public class TaskController
{
    private readonly ITaskRepo _taskRepo;
    private readonly ILog _log;
    
    public TaskController(ITaskRepo taskRepo, ILog log)
    {
        _taskRepo = taskRepo;
        _log = log;
    }

    [HttpGet($"{{id:guid}}")]
    public ActionResult<Todo> Get(Guid id)
    {
        var todo = _taskRepo.Get(id);
        if (todo == null) return new StatusCodeResult(404);

        return todo;
    }

    [HttpPost]
    public ActionResult<Todo> Post([FromBody] TodoRequest todoRequest)
    {
        var todo = Todo.From(todoRequest);
        
        _taskRepo.Insert(todo);

        return todo;
    }
}