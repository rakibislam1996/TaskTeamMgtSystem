using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;

namespace TaskTeamMgtSystem.Infrastructure.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(TaskTeamMgtSystemDbContext db) : base(db) { }

        public async Task<IEnumerable<Team>> GetTeamsWithMembersAsync() => await _db.Teams.Include(t => t.TeamMembers).ToListAsync();

        public async Task<IEnumerable<Team>> GetTeamsByUserAsync(int userId) => await _db.Teams.Where(t => t.TeamMembers.Any(m => m.UserId == userId)).ToListAsync();
    }
}
