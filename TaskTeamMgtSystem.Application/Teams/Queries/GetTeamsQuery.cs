using MediatR;
using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Teams.Queries
{
    public class GetTeamsQuery : IRequest<List<Team>>
    {
    }
}