using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.Features.Users.Queries;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Core.Interfaces.Repositories;
using TaskTeamMgtSystem.Application.Common.Interfaces;

namespace TaskTeamMgtSystem.Application.Features.Users.Handlers
{
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, User>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByEmailAsync(query.Email);
        }
    }
}
