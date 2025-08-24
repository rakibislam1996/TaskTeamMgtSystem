using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Users.Queries;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Users.Handlers
{
    public class GetUsersWithPaginationQueryHandler : IQueryHandler<GetUsersWithPaginationQuery, IPagedResult<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersWithPaginationQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IPagedResult<User>> Handle(GetUsersWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUsersWithPaginationAsync(query.Page, query.PageSize);
        }
    }
}
