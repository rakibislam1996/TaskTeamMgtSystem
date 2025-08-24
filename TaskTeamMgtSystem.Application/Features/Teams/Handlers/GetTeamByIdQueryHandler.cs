using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Teams.Queries;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Handlers
{
    public class GetTeamByIdQueryHandler : IQueryHandler<GetTeamByIdQuery, Team>
    {
        private readonly ITeamRepository _teamRepository;
        public GetTeamByIdQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Team> Handle(GetTeamByIdQuery query, CancellationToken cancellationToken)
        {
            return await _teamRepository.GetByIdAsync(query.Id);
        }
    }
}
