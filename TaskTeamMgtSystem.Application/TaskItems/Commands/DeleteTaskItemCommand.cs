using MediatR;

namespace TaskTeamMgtSystem.Application.TaskItems.Commands
{
    public class DeleteTaskItemCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}