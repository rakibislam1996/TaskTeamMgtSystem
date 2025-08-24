using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Users.Commands;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Users.Handlers
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(command.Id);
            return true;
        }
    }
}
