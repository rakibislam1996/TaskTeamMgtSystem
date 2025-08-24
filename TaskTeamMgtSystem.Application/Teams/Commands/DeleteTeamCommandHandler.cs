using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Teams.Commands
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public DeleteTeamCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _context.Teams.FindAsync(request.Id);
            if (team == null)
                throw new ArgumentException($"Team with ID {request.Id} not found.");

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
