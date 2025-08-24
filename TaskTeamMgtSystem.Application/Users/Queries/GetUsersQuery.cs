using MediatR;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Users.Queries
{
    public class GetUsersQuery : IRequest<List<User>>
    {
    }
}