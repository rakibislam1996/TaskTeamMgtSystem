using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Teams.Commands;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Handlers
{
    public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, int>
    {
        private readonly ITeamRepository _teamRepository;
        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<int> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
        {
            var existing = (await _teamRepository.GetAllAsync()).FirstOrDefault(t => t.Name == command.Name);
            if (existing != null) throw new System.Exception("Team name must be unique.");
            var team = new Team
            {
                Name = command.Name,
                Description = command.Description
            };
            var created = await _teamRepository.AddAsync(team);
            return created.Id;
        }
    }
}
