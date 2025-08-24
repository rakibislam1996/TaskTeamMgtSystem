using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Teams.Queries
{
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Team>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public GetTeamByIdQueryHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Team> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var team = await _context.Teams
                .Include(t => t.TeamMembers)
                .ThenInclude(utm => utm.User)
                .Include(t => t.TaskItems)
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (team == null)
                throw new ArgumentException($"Team with ID {request.Id} not found.");

            return team;
        }
    }
}
