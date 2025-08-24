using TaskTeamMgtSystem.Application.Common.Interfaces;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IQuery<User>
    {
        public int Id { get; set; }
    }
}