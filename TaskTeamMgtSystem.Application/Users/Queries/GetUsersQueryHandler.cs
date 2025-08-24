using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Users.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public GetUsersQueryHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.TeamMappings)
                .ThenInclude(utm => utm.Team)
                .ToListAsync(cancellationToken);
        }
    }
}
