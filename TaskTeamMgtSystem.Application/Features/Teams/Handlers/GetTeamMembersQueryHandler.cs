using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskTeamMgtSystem.Application.Features.Teams.Queries;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Teams.Handlers
{
    public class GetTeamMembersQueryHandler : IQueryHandler<GetTeamMembersQuery, List<User>>
    {
        private readonly ITeamRepository _teamRepository;
        public GetTeamMembersQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<List<User>> Handle(GetTeamMembersQuery query, CancellationToken cancellationToken)
        {
            var teams = await _teamRepository.GetTeamsWithMembersAsync();
            var team = teams.FirstOrDefault(t => t.Id == query.TeamId);
            return team?.TeamMembers?.Select(m => m.User).ToList() ?? new List<User>();
        }
    }
}
