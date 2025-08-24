using MediatR;

namespace TaskTeamMgtSystem.Application.TaskItems.Commands
{
    public class DeleteTaskItemCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}