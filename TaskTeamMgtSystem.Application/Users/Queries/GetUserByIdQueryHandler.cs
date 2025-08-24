using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public GetUserByIdQueryHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.TeamMappings)
                .ThenInclude(utm => utm.Team)
                .Include(u => u.AssignedTasks)
                .Include(u => u.CreatedTasks)
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
                throw new ArgumentException($"User with ID {request.Id} not found.");

            return user;
        }
    }
}
