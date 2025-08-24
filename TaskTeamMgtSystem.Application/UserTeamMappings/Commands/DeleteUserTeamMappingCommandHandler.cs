using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.UserTeamMappings.Commands
{
    public class DeleteUserTeamMappingCommandHandler : IRequestHandler<DeleteUserTeamMappingCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public DeleteUserTeamMappingCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteUserTeamMappingCommand request, CancellationToken cancellationToken)
        {
            var userTeamMapping = await _context.UserTeamMappings
                .FirstOrDefaultAsync(utm => utm.UserId == request.UserId && utm.TeamId == request.TeamId, cancellationToken);

            if (userTeamMapping == null)
                throw new ArgumentException($"User {request.UserId} is not a member of team {request.TeamId}.");

            _context.UserTeamMappings.Remove(userTeamMapping);
            await _context.SaveChangesAsync(cancellationToken);

            return userTeamMapping.Id;
        }
    }
}
