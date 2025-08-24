using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Teams.Commands;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Handlers
{
    public class UpdateTeamCommandHandler : ICommandHandler<UpdateTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;
        public UpdateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(UpdateTeamCommand command, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(command.Id);
            if (team == null) throw new System.Exception("Team not found.");
            team.Name = command.Name;
            team.Description = command.Description;
            await _teamRepository.UpdateAsync(team);
            return true;
        }
    }
}
