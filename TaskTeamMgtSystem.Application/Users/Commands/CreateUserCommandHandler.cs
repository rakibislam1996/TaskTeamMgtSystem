using MediatR;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public CreateUserCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Role = request.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
