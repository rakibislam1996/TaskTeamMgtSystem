using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Teams.Queries
{
    public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, List<Team>>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public GetTeamsQueryHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Include(t => t.TeamMembers)
                .ThenInclude(utm => utm.User)
                .Include(t => t.TaskItems)
                .ToListAsync(cancellationToken);
        }
    }
}
