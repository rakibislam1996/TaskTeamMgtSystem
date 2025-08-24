using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using System.Linq;
using TaskTeamMgtSystem.Core.DTOs.Filters;
using TaskTeamMgtSystem.Core.Domain.Enums;

namespace TaskTeamMgtSystem.Infrastructure.Repositories
{
    public class TasksRepository : GenericRepository<TaskItem>, ITasksRepository
    {
        public TasksRepository(TaskTeamMgtSystemDbContext db) : base(db) { }

        public async Task<IEnumerable<TaskItem>> GetTasksByAssigneeAsync(int assignedToUserId) => await _db.Tasks.Where(t => t.AssignedToUserId == assignedToUserId).ToListAsync();

        public async Task<IEnumerable<TaskItem>> GetTasksByTeamAsync(int teamId) => await _db.Tasks.Where(t => t.TeamId == teamId).ToListAsync();

        public async Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(TasksStatus status) => await _db.Tasks.Where(t => t.Status == status).ToListAsync();

        public async Task<IEnumerable<TaskItem>> GetTasksWithFiltersAsync(TaskFilterParameters filters)
        {
            var query = _db.Tasks.AsQueryable();
            if (filters.Status.HasValue)
                query = query.Where(t => t.Status == filters.Status.Value);
            if (filters.AssignedToUserId.HasValue)
                query = query.Where(t => t.AssignedToUserId == filters.AssignedToUserId.Value);
            if (filters.TeamId.HasValue)
                query = query.Where(t => t.TeamId == filters.TeamId.Value);
            if (filters.DueDateFrom.HasValue)
                query = query.Where(t => t.DueDate >= filters.DueDateFrom.Value);
            if (filters.DueDateTo.HasValue)
                query = query.Where(t => t.DueDate <= filters.DueDateTo.Value);
            return await query.ToListAsync();
        }

        public async Task<IPagedResult<TaskItem>> GetTasksWithPaginationAndSortingAsync(int page, int pageSize, string sortBy, string sortDirection)
        {
            var query = _db.Tasks.AsQueryable();
            if (sortBy == "DueDate")
                query = sortDirection == "desc" ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate);
            else if (sortBy == "Title")
                query = sortDirection == "desc" ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title);
            return await GetPagedAsync(page, pageSize);
        }
    }
}
