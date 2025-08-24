using System.Threading;
using System.Threading.Tasks;

namespace TaskTeamMgtSystem.Application.Common.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}