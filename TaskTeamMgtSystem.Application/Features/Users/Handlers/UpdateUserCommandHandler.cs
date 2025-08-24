using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Users.Commands;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Users.Handlers
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user == null) throw new System.Exception("User not found.");
            user.FullName = command.FullName;
            user.Email = command.Email;
            user.Role = command.Role;
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
