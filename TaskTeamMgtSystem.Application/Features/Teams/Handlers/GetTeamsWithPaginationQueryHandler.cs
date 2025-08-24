using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Teams.Queries;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Handlers
{
    public class GetTeamsWithPaginationQueryHandler : IQueryHandler<GetTeamsWithPaginationQuery, IPagedResult<Team>>
    {
        private readonly ITeamRepository _teamRepository;
        public GetTeamsWithPaginationQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IPagedResult<Team>> Handle(GetTeamsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _teamRepository.GetPagedAsync(query.Page, query.PageSize);
        }
    }
}
