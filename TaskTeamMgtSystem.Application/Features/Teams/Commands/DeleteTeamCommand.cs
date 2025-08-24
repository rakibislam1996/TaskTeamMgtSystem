using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Commands
{
    public class DeleteTeamCommand : ICommand<bool>
    {
        public int Id { get; set; }
    }
}