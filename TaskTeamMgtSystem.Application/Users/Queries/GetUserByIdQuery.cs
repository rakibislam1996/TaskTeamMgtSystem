using MediatR;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
}