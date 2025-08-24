using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Teams.Commands
{
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public UpdateTeamCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.FindAsync(request.Id);
            if (team == null)
                throw new ArgumentException($"Team with ID {request.Id} not found.");

            team.Name = request.Name;
            team.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
            return team.Id;
        }
    }
}
