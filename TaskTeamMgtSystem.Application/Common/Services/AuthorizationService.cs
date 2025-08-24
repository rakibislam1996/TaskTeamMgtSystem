using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Application.Common.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly TaskTeamMgtSystemDbContext _context;

        public AuthorizationService(TaskTeamMgtSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CanUserAccessTaskAsync(int userId, int taskId, CancellationToken cancellationToken = default)
        {
            var task = await _context.TaskItem
                .Include(t => t.AssignedTo)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);

            if (task == null) return false;

            // User can access task if:
            // 1. They are assigned to the task
            // 2. They created the task
            // 3. They are a manager/admin in the same team
            return task.AssignedToUserId == userId ||
                   task.CreatedByUserId == userId ||
                   await CanUserManageTeamAsync(userId, task.TeamId, cancellationToken);
        }

        public async Task<bool> CanUserManageTeamAsync(int userId, int teamId, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(userId, cancellationToken);
            if (user == null) return false;

            // Admin can manage any team
            if (user.Role == "Admin") return true;

            // Manager can manage teams they are part of
            if (user.Role == "Manager")
            {
                return await IsUserInTeamAsync(userId, teamId, cancellationToken);
            }

            return false;
        }

        public async Task<bool> IsUserInTeamAsync(int userId, int teamId, CancellationToken cancellationToken = default)
        {
            return await _context.UserTeamMappings
                .AnyAsync(utm => utm.UserId == userId && utm.TeamId == teamId, cancellationToken);
        }
    }
}
