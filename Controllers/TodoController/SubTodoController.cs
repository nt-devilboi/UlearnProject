using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.Domen.Entities;
using MediatR;
using UlearnTodoTimer.Controllers.Model;


namespace UlearnTodoTimer.Controllers
{
    /*[ApiController]
    [Route("api/[controller]")]
    public class SubTodoController : Controller
    {
        private readonly ISubTodoRepo _repo;
        private readonly ILog _log;

        public SubTodoController(ISubTodoRepo repo, ILog log, IMediator mediator)
        {
            _repo = repo;
            _log = log;
        }

        [HttpGet($"{{id:guid}}")]
        public async Task<ActionResult<SubTodo>> Get(Guid id)
        {
            var subTodo = await _repo.Get(id);
            if (subTodo == null) return new StatusCodeResult(404);

            return subTodo;
        }

        [HttpPost]
        public async Task<ActionResult<SubTodo>> Post([FromBody] SubTodoRequest subTodoRequest)
        {
            var subTodo = SubTodo.From(subTodoRequest);
            await _repo.Insert(subTodo);

            return subTodo;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubTodo>>> GetAll()
        {
            var subTodos = await _repo.GetAll();

            return subTodos;
        }

        [HttpDelete($"{{id:guid}}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _repo.Delete(id);

            return new OkResult();
        }

        [HttpPatch("{id:guid}/do")]
        public async Task<ActionResult> Patch(Guid id)
        {
            var todo = await _repo.Get(id);

            todo.IsComplete = !todo.IsComplete;

            _repo.Update(todo);

            return new OkResult();
        }
    }*/
}
