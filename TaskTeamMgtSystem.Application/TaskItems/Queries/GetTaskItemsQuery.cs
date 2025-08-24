using MediatR;
using System;
using System.Collections.Generic;
using TaskTeamMgtSystem.Application.Common.Models;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.TaskItems.Queries
{
    public class GetTaskItemsQuery : IRequest<PaginatedResult<TaskItem>>
    {
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? TeamId { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }
    }
}