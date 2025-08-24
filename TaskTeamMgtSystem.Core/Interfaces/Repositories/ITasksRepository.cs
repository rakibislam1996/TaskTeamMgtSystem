using System.Threading.Tasks;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Domain.Enums;
using TaskTeamMgtSystem.Core.DTOs.Filters;

namespace TaskTeamMgtSystem.Core.Interfaces.Repositories
{
    public interface ITasksRepository : IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetTasksByAssigneeAsync(int assignedToUserId);
        Task<IEnumerable<TaskItem>> GetTasksByTeamAsync(int teamId);
        Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(TasksStatus status);
        Task<IEnumerable<TaskItem>> GetTasksWithFiltersAsync(TaskFilterParameters filters);
        Task<IPagedResult<TaskItem>> GetTasksWithPaginationAndSortingAsync(int page, int pageSize, string sortBy, string sortDirection);
    }
}
