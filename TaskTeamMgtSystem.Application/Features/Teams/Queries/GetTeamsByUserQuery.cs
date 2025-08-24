using TaskTeamMgtSystem.Application.Common.Interfaces;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Features.Teams.Queries
{
    public class GetTeamsByUserQuery : IQuery<List<Team>>
    {
        public int UserId { get; set; }
    }
}