using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UlearnTodoTimer.Application;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.Domen.Entities;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.Controllers;


//todo: код написан упрощенно, ибо мне пока лень думать, но концептуально все именно так. как минимум нету проверок ваще. если не понятно. либо спросить меня, либо phind.com`
[ApiController]
[Route("api/tasks")]
public class ToDoController
{
    private readonly ITodoRepo _todoRepo;
    private readonly ILog _log;
    private readonly UserInfoScope _userInfoScope;
    private readonly IMediator _mediator;
    public ToDoController(ITodoRepo todoRepo, ILog log, UserInfoScope userInfoScope, IMediator mediator)
    {
        _todoRepo = todoRepo;
        _log = log;
        _userInfoScope = userInfoScope;
        _mediator = mediator;
    }

    [HttpGet($"{{id:guid}}")]
    public async Task<ActionResult<Todo>> Get(Guid id)
    {
        var todo = await _todoRepo.Get(id);
        if (todo == null) return new StatusCodeResult(404);

        if (todo.TokenId != _userInfoScope.Token.id) return new StatusCodeResult(403);
        
        
        return todo;
    }

    [HttpPost]
    public ActionResult<Todo> Post([FromBody] TodoRequest todoRequest)
    {
        var todo = Todo.From(todoRequest, _userInfoScope.Token.id);

        _mediator.Send(new AddTodoCommand(todo)); // релизован здесь про приколу, чтоб просто был. не вижу смысла реализовывать все, ибо кринж, в нем только Insert написан.

        return todo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAll()
    {
        var todos = await _todoRepo.GetAll(_userInfoScope.Token.id);

        return todos;
    }

    [HttpDelete($"{{id:guid}}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _todoRepo.Delete(id);

        return new OkResult();
    }

    [HttpPatch("{id:guid}/do")]
    public async Task<ActionResult> Patch(Guid id)
    {
        var todo = await _todoRepo.Get(id);
        
        todo.IsComplete = !todo.IsComplete;

        _todoRepo.Update(todo);

        return new OkResult();
    }
}