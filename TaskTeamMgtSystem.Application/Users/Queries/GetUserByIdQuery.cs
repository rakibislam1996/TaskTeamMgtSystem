using MediatR;
using TaskTeamMgtSystem.Application.Users.DTOs;

namespace TaskTeamMgtSystem.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}