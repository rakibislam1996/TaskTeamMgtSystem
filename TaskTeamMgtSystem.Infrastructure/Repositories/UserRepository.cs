using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;

namespace TaskTeamMgtSystem.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TaskTeamMgtSystemDbContext db) : base(db) { }

        public async Task<User?> GetByEmailAsync(string email) => await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<IEnumerable<User>> GetByRoleAsync(string role) => await _db.Users.Where(u => u.Role == role).ToListAsync();

        public async Task<IPagedResult<User>> GetUsersWithPaginationAsync(int page, int pageSize) => await GetPagedAsync(page, pageSize);
    }
}
