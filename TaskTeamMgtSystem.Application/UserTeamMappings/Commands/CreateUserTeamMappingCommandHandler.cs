using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.UserTeamMappings.Commands
{
    public class CreateUserTeamMappingCommandHandler : IRequestHandler<CreateUserTeamMappingCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public CreateUserTeamMappingCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateUserTeamMappingCommand request, CancellationToken cancellationToken)
        {
            // Validate that the user exists
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                throw new ArgumentException($"User with ID {request.UserId} not found.");

            // Validate that the team exists
            var team = await _context.Teams.FindAsync(request.TeamId);
            if (team == null)
                throw new ArgumentException($"Team with ID {request.TeamId} not found.");

            // Check if mapping already exists
            var existingMapping = await _context.UserTeamMappings
                .FirstOrDefaultAsync(utm => utm.UserId == request.UserId && utm.TeamId == request.TeamId, cancellationToken);

            if (existingMapping != null)
                throw new InvalidOperationException($"User {request.UserId} is already a member of team {request.TeamId}.");

            var userTeamMapping = new UserTeamMapping
            {
                UserId = request.UserId,
                TeamId = request.TeamId
            };

            _context.UserTeamMappings.Add(userTeamMapping);
            await _context.SaveChangesAsync(cancellationToken);

            return userTeamMapping.Id;
        }
    }
}
