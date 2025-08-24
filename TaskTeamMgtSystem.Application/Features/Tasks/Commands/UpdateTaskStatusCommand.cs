using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Tasks.Commands
{
    public class UpdateTaskStatusCommand : ICommand<bool>
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}