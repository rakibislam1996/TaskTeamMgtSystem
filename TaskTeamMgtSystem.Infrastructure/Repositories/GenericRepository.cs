using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Common;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Infrastructure;

namespace TaskTeamMgtSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly TaskTeamMgtSystemDbContext _db;
        public GenericRepository(TaskTeamMgtSystemDbContext db)
        {
            _db = db;
        }

        public async Task<T?> GetByIdAsync(int id) => await _db.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _db.Set<T>().ToListAsync();

        public async Task<IPagedResult<T>> GetPagedAsync(int page, int pageSize)
        {
            var items = await _db.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var totalCount = await _db.Set<T>().CountAsync();
            return new PagedResult<T>(items, totalCount, page, pageSize);
        }

        public async Task<T> AddAsync(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _db.Set<T>().Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id) => await _db.Set<T>().AnyAsync(e => e.Id == id);
    }

    public class PagedResult<T> : IPagedResult<T>
    {
        public IEnumerable<T> Items { get; }
        public int TotalCount { get; }
        public int Page { get; }
        public int PageSize { get; }
        public PagedResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }
    }
}
