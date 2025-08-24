using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Commands
{
    public class CreateTeamCommand : ICommand<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}