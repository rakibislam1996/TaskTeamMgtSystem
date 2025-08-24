using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using System.Linq;

namespace TaskTeamMgtSystem.Infrastructure.Repositories
{
    public class UserTeamMappingRepository : GenericRepository<UserTeamMapping>, IUserTeamMappingRepository
    {
        public UserTeamMappingRepository(TaskTeamMgtSystemDbContext db) : base(db) { }

        public async Task<IEnumerable<UserTeamMapping>> GetByUserIdAsync(int userId) => await _db.UserTeamMappings.Where(m => m.UserId == userId).ToListAsync();

        public async Task<IEnumerable<UserTeamMapping>> GetByTeamIdAsync(int teamId) => await _db.UserTeamMappings.Where(m => m.TeamId == teamId).ToListAsync();

        public async Task<UserTeamMapping?> GetMappingAsync(int userId, int teamId) => await _db.UserTeamMappings.FirstOrDefaultAsync(m => m.UserId == userId && m.TeamId == teamId);

        public async Task RemoveMappingAsync(int userId, int teamId)
        {
            var mapping = await GetMappingAsync(userId, teamId);
            if (mapping != null)
            {
                _db.UserTeamMappings.Remove(mapping);
                await _db.SaveChangesAsync();
            }
        }
    }
}
