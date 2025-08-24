using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Domain.Enums;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.TaskItems.Commands
{
    public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand, int>
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public UpdateTaskItemCommandHandler(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.TaskItem.FindAsync(request.Id);
            if (taskItem == null)
                throw new ArgumentException($"TaskItem with ID {request.Id} not found.");

            // Validate that the assigned user exists if it's being changed
            if (request.AssignedToUserId != taskItem.AssignedToUserId)
            {
                var assignedUser = await _context.Users.FindAsync(request.AssignedToUserId);
                if (assignedUser == null)
                    throw new ArgumentException($"User with ID {request.AssignedToUserId} not found.");
                taskItem.AssignedToUserId = request.AssignedToUserId;
            }

            // Validate that the team exists if it's being changed
            if (request.TeamId != taskItem.TeamId)
            {
                var team = await _context.Teams.FindAsync(request.TeamId);
                if (team == null)
                    throw new ArgumentException($"Team with ID {request.TeamId} not found.");
                taskItem.TeamId = request.TeamId;
            }

            taskItem.Title = request.Title;
            taskItem.Description = request.Description;
            taskItem.DueDate = request.DueDate;
            taskItem.Status = Enum.Parse<TaskTeamMgtSystem.Core.Domain.Enums.TaskStatus>(request.Status);
            taskItem.Priority = request.Priority;

            await _context.SaveChangesAsync(cancellationToken);
            return taskItem.Id;
        }
    }
}
