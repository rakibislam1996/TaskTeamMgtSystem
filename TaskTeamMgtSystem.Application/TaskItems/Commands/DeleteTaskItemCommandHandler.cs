using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.TaskItems.Commands
{
    public class DeleteTaskItemCommandHandler : IRequestHandler<DeleteTaskItemCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public DeleteTaskItemCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.TaskItem.FindAsync(request.Id);
            if (taskItem == null)
                throw new ArgumentException($"TaskItem with ID {request.Id} not found.");

            _context.TaskItem.Remove(taskItem);
            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
