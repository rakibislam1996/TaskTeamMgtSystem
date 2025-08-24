using TaskTeamMgtSystem.Application.Common.Interfaces;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Features.Tasks.Queries
{
    public class GetMyTasksQuery : IQuery<List<TaskItem>>
    {
        public int UserId { get; set; }
    }
}