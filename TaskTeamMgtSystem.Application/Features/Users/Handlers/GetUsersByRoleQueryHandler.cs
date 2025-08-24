using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskTeamMgtSystem.Application.Features.Users.Queries;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Users.Handlers
{
    public class GetUsersByRoleQueryHandler : IQueryHandler<GetUsersByRoleQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersByRoleQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Handle(GetUsersByRoleQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetByRoleAsync(query.Role);
            return new List<User>(users);
        }
    }
}
