using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTeamMgtSystem.Application.UserTeamMappings.Commands;
using TaskTeamMgtSystem.Application.UserTeamMappings.Queries;

namespace TaskTeamMgtSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
    public class UserTeamMappingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserTeamMappingController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetMappings([FromQuery] GetUserTeamMappingsQuery query) => Ok(await _mediator.Send(query));

        [HttpPost]
        public async Task<IActionResult> CreateMapping(CreateUserTeamMappingCommand command) => Ok(await _mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapping(int id) => Ok(await _mediator.Send(new DeleteUserTeamMappingCommand { Id = id }));
    }
}
