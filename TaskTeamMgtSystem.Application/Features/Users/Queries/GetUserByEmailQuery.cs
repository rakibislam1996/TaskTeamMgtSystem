using TaskTeamMgtSystem.Application.Common.Interfaces;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Features.Users.Queries
{
    public class GetUserByEmailQuery : IQuery<User>
    {
        public string Email { get; set; }
    }
}