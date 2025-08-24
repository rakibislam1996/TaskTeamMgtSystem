using TaskTeamMgtSystem.Application.Common.Interfaces;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Features.Teams.Queries
{
    public class GetTeamMembersQuery : IQuery<List<User>>
    {
        public int TeamId { get; set; }
    }
}