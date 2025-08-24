using TaskTeamMgtSystem.Application.Common.Interfaces;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;

namespace TaskTeamMgtSystem.Application.Features.Users.Queries
{
    public class GetUsersWithPaginationQuery : IQuery<IPagedResult<User>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}