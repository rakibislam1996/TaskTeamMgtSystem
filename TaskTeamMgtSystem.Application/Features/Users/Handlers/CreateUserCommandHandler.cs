using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Users.Commands;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace TaskTeamMgtSystem.Application.Features.Users.Handlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var existing = await _userRepository.GetByEmailAsync(command.Email);
            if (existing != null) throw new System.Exception("Email already exists.");
            var passwordHasher = new PasswordHasher<User>();
            var user = new User
            {
                FullName = command.FullName,
                Email = command.Email,
                Role = command.Role,
                PasswordHash = passwordHasher.HashPassword(null, command.Password)
            };
            var created = await _userRepository.AddAsync(user);
            return created.Id;
        }
    }
}
