using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Tasks.Commands
{
    public class DeleteTaskCommand : ICommand<bool>
    {
        public int Id { get; set; }
    }
}