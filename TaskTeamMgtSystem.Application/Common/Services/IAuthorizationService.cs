using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Common.Services
{
    public interface IAuthorizationService
    {
        Task<bool> CanUserAccessTaskAsync(int userId, int taskId, CancellationToken cancellationToken = default);
        Task<bool> CanUserManageTeamAsync(int userId, int teamId, CancellationToken cancellationToken = default);
        Task<bool> IsUserInTeamAsync(int userId, int teamId, CancellationToken cancellationToken = default);
    }
}
