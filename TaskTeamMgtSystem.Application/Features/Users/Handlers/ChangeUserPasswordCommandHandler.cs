using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Users.Commands;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Features.Users.Handlers
{
    public class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null) throw new System.Exception("User not found.");
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, command.NewPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
