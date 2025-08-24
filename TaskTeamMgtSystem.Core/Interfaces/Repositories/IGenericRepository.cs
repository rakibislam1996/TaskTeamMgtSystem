using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Core.Domain.Common;

namespace TaskTeamMgtSystem.Core.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IPagedResult<T>> GetPagedAsync(int page, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
