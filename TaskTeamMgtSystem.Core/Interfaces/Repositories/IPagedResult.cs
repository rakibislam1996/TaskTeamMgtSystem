using System.Collections.Generic;

namespace TaskTeamMgtSystem.Core.Interfaces.Repositories
{
    public interface IPagedResult<T>
    {
        IEnumerable<T> Items { get; }
        int TotalCount { get; }
        int Page { get; }
        int PageSize { get; }
    }
}
