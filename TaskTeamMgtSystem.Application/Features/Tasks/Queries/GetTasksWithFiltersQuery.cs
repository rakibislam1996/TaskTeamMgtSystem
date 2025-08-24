using TaskTeamMgtSystem.Application.Common.Interfaces;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.DTOs.Filters;

namespace TaskTeamMgtSystem.Application.Features.Tasks.Queries
{
    public class GetTasksWithFiltersQuery : IQuery<List<TaskItem>>
    {
        public TaskFilterParameters Filters { get; set; }
    }
}