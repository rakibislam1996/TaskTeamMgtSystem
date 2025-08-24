using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.UserTeamMappings.Queries
{
    public class GetUserTeamMappingsQueryHandler : IRequestHandler<GetUserTeamMappingsQuery, List<UserTeamMapping>>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public GetUserTeamMappingsQueryHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserTeamMapping>> Handle(GetUserTeamMappingsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.UserTeamMappings
                .Include(utm => utm.User)
                .Include(utm => utm.Team)
                .AsQueryable();

            // Apply filters
            if (request.UserId.HasValue)
                query = query.Where(utm => utm.UserId == request.UserId.Value);

            if (request.TeamId.HasValue)
                query = query.Where(utm => utm.TeamId == request.TeamId.Value);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
