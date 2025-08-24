using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Commands
{
    public class RemoveUserFromTeamCommand : ICommand<bool>
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}