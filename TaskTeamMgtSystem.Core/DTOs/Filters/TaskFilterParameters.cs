using System;
using TaskTeamMgtSystem.Core.Domain.Enums;

namespace TaskTeamMgtSystem.Core.DTOs.Filters
{
    public class TaskFilterParameters
    {
        public TasksStatus? Status { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? TeamId { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Id";
        public string SortDirection { get; set; } = "asc";
    }
}
