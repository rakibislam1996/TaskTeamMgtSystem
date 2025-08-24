using System.Threading.Tasks;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Core.Interfaces.Repositories
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        Task<IEnumerable<Team>> GetTeamsWithMembersAsync();
        Task<IEnumerable<Team>> GetTeamsByUserAsync(int userId);
    }
}
