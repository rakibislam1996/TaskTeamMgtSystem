using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.TaskItems.Queries
{
    public class GetTaskItemsQueryHandler : IRequestHandler<GetTaskItemsQuery, List<TaskItem>>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public GetTaskItemsQueryHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> Handle(GetTaskItemsQuery request, CancellationToken cancellationToken)
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

            return await query.ToListAsync(cancellationToken);
        }
    }
}
