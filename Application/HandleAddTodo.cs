using MediatR;
using MediatR.Pipeline;
using UlearnTodoTimer.Domen.Entities;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.Application;

//Warn код больше для эксперемента для понимания как рабаоет mediator, мб в будущем будем использовать, пока не очень понятно для чего его использовать)
public class AddTodoHandler : IRequestHandler<AddTodoCommand>
{
    private readonly ITodoRepo _repo;
    private readonly ILog _log;
    public AddTodoHandler(ITodoRepo repo, ILog log)
    {
        _repo = repo;
        _log = log;
    }

    public async Task Handle(AddTodoCommand request, CancellationToken cancellationToken)
    {
        _log.Info("in Handle");
       
    }
}


public class AddTodoHanlerPre : IRequestPreProcessor<AddTodoCommand>
{
    private readonly ILog _log;

    public AddTodoHanlerPre(ILog log)
    {
        _log = log;
    }

    public async Task Process(AddTodoCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("in preProcess");
        _log.Info("in preProcess");
    }
}

public record AddTodoCommand(Todo Todo) : IRequest;

