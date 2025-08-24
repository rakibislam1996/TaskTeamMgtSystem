using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Users.Commands
{
    public class DeleteUserCommand : ICommand<bool>
    {
        public int Id { get; set; }
    }
}