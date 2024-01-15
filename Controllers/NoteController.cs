using Microsoft.AspNetCore.Mvc;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.Domen.Entities;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.Controllers;

public class NoteController:Controller
{
    private readonly INoteRepo _repo;
    private readonly ILog _log;
    private readonly UserInfoScope _accountScope;

    public NoteController(INoteRepo repo, ILog log, UserInfoScope accountScope)
    {
        _repo = repo;
        _log = log;
        _accountScope = accountScope;
    }

    [HttpGet($"{{id:guid}}")]
    public async Task<ActionResult<Note>> Get(Guid id)
    {
        var todo = await _repo.Get(id);
        if (todo == null) return new StatusCodeResult(404);

        return todo;
    }

     [HttpPost]
     public ActionResult<Note> Post([FromBody] NoteRequest noteRequest)
     {
         var note = Note.From(noteRequest);
    
         _repo.Insert(note);
    
         return note;
     }

    [HttpDelete($"{{id:guid}}")]
    public async Task<ActionResult<Note>>Delete(Guid id)
    {
        await _repo.Delete(id);
        return new OkResult();

    }
}