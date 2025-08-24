using System.Threading.Tasks;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Core.Interfaces.Repositories
{
    public interface IUserTeamMappingRepository : IGenericRepository<UserTeamMapping>
    {
        Task<IEnumerable<UserTeamMapping>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserTeamMapping>> GetByTeamIdAsync(int teamId);
        Task<UserTeamMapping?> GetMappingAsync(int userId, int teamId);
        Task RemoveMappingAsync(int userId, int teamId);
    }
}
