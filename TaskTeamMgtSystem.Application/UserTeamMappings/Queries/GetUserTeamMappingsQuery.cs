using MediatR;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.UserTeamMappings.Queries
{
    public class GetUserTeamMappingsQuery : IRequest<List<UserTeamMapping>>
    {
        public int? UserId { get; set; }
        public int? TeamId { get; set; }
    }
}