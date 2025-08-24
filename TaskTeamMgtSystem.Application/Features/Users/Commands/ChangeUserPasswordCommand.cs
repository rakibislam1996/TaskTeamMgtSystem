using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Users.Commands
{
    public class ChangeUserPasswordCommand : ICommand<bool>
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
}