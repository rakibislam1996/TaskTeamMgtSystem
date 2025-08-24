using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskTeamMgtSystem.Application.Features.Teams.Queries;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Handlers
{
    public class GetTeamsByUserQueryHandler : IQueryHandler<GetTeamsByUserQuery, List<Team>>
    {
        private readonly ITeamRepository _teamRepository;
        public GetTeamsByUserQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<List<Team>> Handle(GetTeamsByUserQuery query, CancellationToken cancellationToken)
        {
            var teams = await _teamRepository.GetTeamsByUserAsync(query.UserId);
            return new List<Team>(teams);
        }
    }
}
