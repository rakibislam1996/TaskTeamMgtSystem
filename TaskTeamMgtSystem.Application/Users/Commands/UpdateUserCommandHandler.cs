using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public UpdateUserCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
                throw new ArgumentException($"User with ID {request.Id} not found.");

            user.FullName = request.FullName;
            user.Email = request.Email;
            user.Role = request.Role;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
