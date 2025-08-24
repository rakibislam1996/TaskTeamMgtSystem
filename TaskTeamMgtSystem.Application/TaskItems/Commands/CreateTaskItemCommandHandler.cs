using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Domain.Enums;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.TaskItems.Commands
{
    public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public CreateTaskItemCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            // Validate that the assigned user exists
            var assignedUser = await _context.Users.FindAsync(request.AssignedToUserId);
            if (assignedUser == null)
                throw new ArgumentException($"User with ID {request.AssignedToUserId} not found.");

            // Validate that the team exists
            var team = await _context.Teams.FindAsync(request.TeamId);
            if (team == null)
                throw new ArgumentException($"Team with ID {request.TeamId} not found.");

            var taskItem = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                AssignedToUserId = request.AssignedToUserId,
                CreatedByUserId = request.CreatedByUserId,
                TeamId = request.TeamId,
                DueDate = request.DueDate,
                Status = Enum.Parse<TaskStatus>(request.Status),
                AssignedTo = assignedUser,
                CreatedBy = assignedUser, // For now, using assigned user as creator
                Team = team
            };

            _context.TaskItem.Add(taskItem);
            await _context.SaveChangesAsync(cancellationToken);

            return taskItem.Id;
        }
    }
}
