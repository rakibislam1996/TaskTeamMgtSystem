using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Teams.Commands;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Handlers
{
    public class DeleteTeamCommandHandler : ICommandHandler<DeleteTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;
        public DeleteTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
        {
            await _teamRepository.DeleteAsync(command.Id);
            return true;
        }
    }
}
