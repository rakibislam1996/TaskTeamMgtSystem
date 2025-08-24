using MediatR;
using System.Collections.Generic;
using TaskTeamMgtSystem.Application.Users.DTOs;

namespace TaskTeamMgtSystem.Application.Users.Queries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
    }
}