using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.Domen.Entities;
using MediatR;
using UlearnTodoTimer.Infrasturcture.Repositories.SubTodoRepo;
using UlearnTodoTimer.Controllers.Model;


namespace UlearnTodoTimer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubTodoController : Controller
    {
        private readonly ISubTodoRepo _repo;
        private readonly ILog _log;
        private readonly UserInfoScope _userInfoScope;

        public SubTodoController(ISubTodoRepo repo, ILog log, UserInfoScope userInfoScope, IMediator mediator)
        {
            _repo = repo;
            _log = log;
            _userInfoScope = userInfoScope;
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
            var subTodo = SubTodo.From(subTodoRequest, _userInfoScope.Token.id);
            await _repo.Insert(subTodo);

            return subTodo;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubTodo>>> GetAll()
        {
            var subTodos = await _repo.GetAll(_userInfoScope.Token.id);

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

        /*// GET: SubToDoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubToDoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubToDoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubToDoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubToDoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubToDoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubToDoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
