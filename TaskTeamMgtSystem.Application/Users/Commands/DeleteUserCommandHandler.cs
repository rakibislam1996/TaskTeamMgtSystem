using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Users.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public DeleteUserCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
                throw new ArgumentException($"User with ID {request.Id} not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
