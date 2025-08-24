using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Commands
{
    public class AddUserToTeamCommand : ICommand<bool>
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}