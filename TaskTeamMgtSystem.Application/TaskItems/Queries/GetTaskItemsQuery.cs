using MediatR;
using System;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.TaskItems.Queries
{
    public class GetTaskItemsQuery : IRequest<List<Tasks>>
    {
        public string Status { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? TeamId { get; set; }
        public DateTime? DueDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }
    }
}