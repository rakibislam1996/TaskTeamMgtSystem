using MediatR;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.TaskItems.Queries
{
    public class GetTaskItemByIdQuery : IRequest<Tasks>
    {
        public int Id { get; set; }
    }
}