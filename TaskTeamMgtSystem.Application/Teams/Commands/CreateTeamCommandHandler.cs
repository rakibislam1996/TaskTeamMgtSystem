using MediatR;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Teams.Commands
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public CreateTeamCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new Team
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync(cancellationToken);

            return team.Id;
        }
    }
}
