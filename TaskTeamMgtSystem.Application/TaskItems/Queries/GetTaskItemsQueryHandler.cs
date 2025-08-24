using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Application.Common.Models;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.TaskItems.Queries
{
    public class GetTaskItemsQueryHandler : IRequestHandler<GetTaskItemsQuery, PaginatedResult<TaskItem>>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public GetTaskItemsQueryHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<TaskItem>> Handle(GetTaskItemsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.TaskItem
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .Include(t => t.Team)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(request.Status))
                query = query.Where(t => t.Status.ToString() == request.Status);

            if (!string.IsNullOrEmpty(request.Priority))
                query = query.Where(t => t.Priority == request.Priority);

            if (request.TeamId.HasValue)
                query = query.Where(t => t.TeamId == request.TeamId.Value);

            if (request.AssignedToUserId.HasValue)
                query = query.Where(t => t.AssignedToUserId == request.AssignedToUserId.Value);

            if (request.CreatedByUserId.HasValue)
                query = query.Where(t => t.CreatedByUserId == request.CreatedByUserId.Value);

            if (request.DueDateFrom.HasValue)
                query = query.Where(t => t.DueDate >= request.DueDateFrom.Value);

            if (request.DueDateTo.HasValue)
                query = query.Where(t => t.DueDate <= request.DueDateTo.Value);

            // Apply sorting
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                query = request.SortBy.ToLower() switch
                {
                    "title" => request.SortDesc ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
                    "duedate" => request.SortDesc ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate),
                    "status" => request.SortDesc ? query.OrderByDescending(t => t.Status) : query.OrderBy(t => t.Status),
                    "priority" => request.SortDesc ? query.OrderByDescending(t => t.Priority) : query.OrderBy(t => t.Priority),
                    "assignedto" => request.SortDesc ? query.OrderByDescending(t => t.AssignedTo.FullName) : query.OrderBy(t => t.AssignedTo.FullName),
                    "team" => request.SortDesc ? query.OrderByDescending(t => t.Team.Name) : query.OrderBy(t => t.Team.Name),
                    _ => query.OrderBy(t => t.Id)
                };
            }
            else
            {
                query = query.OrderBy(t => t.Id);
            }

            // Apply pagination
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // Return with pagination info
            return new PaginatedResult<TaskItem>(items, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
