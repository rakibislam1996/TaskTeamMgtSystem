using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.TaskItems.Queries
{
    public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItem>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public GetTaskItemByIdQueryHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.TaskItem
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (taskItem == null)
                throw new ArgumentException($"TaskItem with ID {request.Id} not found.");

            return taskItem;
        }
    }
}
