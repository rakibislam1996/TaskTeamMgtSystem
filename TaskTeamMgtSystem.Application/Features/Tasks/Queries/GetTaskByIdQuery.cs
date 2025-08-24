using TaskTeamMgtSystem.Application.Common.Interfaces;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Features.Tasks.Queries
{
    public class GetTaskByIdQuery : IQuery<TaskItem>
    {
        public int Id { get; set; }
    }
}