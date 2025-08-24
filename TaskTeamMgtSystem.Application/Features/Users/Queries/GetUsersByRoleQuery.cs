using TaskTeamMgtSystem.Application.Common.Interfaces;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Features.Users.Queries
{
    public class GetUsersByRoleQuery : IQuery<List<User>>
    {
        public string Role { get; set; }
    }
}