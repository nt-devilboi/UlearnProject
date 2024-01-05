using UlearnTodoTimer.Domen.Entities;

namespace UlearnTodoTimer.Infrasturcture.Repositories.SubTodoRepo
{
    public class SubTodoRepoFake : ISubTodoRepo
    {
        private List<SubTodo> _list;
        public SubTodoRepoFake()
        {
            _list = new List<SubTodo>();
        }

        public Task<SubTodo?> Get(Guid id)
        {
            return Task.FromResult(_list.FirstOrDefault(t => t.Id == id));
        }

        public Task<List<SubTodo>> GetAll(Guid user)
        {
            return Task.FromResult(_list);
        }

        public Task Insert(SubTodo subTodo)
        {
            _list.Add(subTodo);
            return Task.CompletedTask;
        }

        public void Update(SubTodo? subTodo)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
