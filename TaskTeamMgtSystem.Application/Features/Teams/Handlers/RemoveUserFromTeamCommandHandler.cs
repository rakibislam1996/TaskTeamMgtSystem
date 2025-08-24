using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Teams.Commands;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Handlers
{
    public class RemoveUserFromTeamCommandHandler : ICommandHandler<RemoveUserFromTeamCommand, bool>
    {
        private readonly IUserTeamMappingRepository _mappingRepository;
        public RemoveUserFromTeamCommandHandler(IUserTeamMappingRepository mappingRepository)
        {
            _mappingRepository = mappingRepository;
        }

        public async Task<bool> Handle(RemoveUserFromTeamCommand command, CancellationToken cancellationToken)
        {
            await _mappingRepository.RemoveMappingAsync(command.UserId, command.TeamId);
            return true;
        }
    }
}
