using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Teams.Commands;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Handlers
{
    public class AddUserToTeamCommandHandler : ICommandHandler<AddUserToTeamCommand, bool>
    {
        private readonly IUserTeamMappingRepository _mappingRepository;
        public AddUserToTeamCommandHandler(IUserTeamMappingRepository mappingRepository)
        {
            _mappingRepository = mappingRepository;
        }

        public async Task<bool> Handle(AddUserToTeamCommand command, CancellationToken cancellationToken)
        {
            var existing = await _mappingRepository.GetMappingAsync(command.UserId, command.TeamId);
            if (existing != null) throw new System.Exception("User already assigned to team.");
            await _mappingRepository.AddAsync(new TaskTeamMgtSystem.Core.Domain.Entities.UserTeamMapping
            {
                UserId = command.UserId,
                TeamId = command.TeamId
            });
            return true;
        }
    }
}
