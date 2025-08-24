using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTeamMgtSystem.Application.Users.Commands;
using TaskTeamMgtSystem.Application.Users.Queries;

namespace TaskTeamMgtSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetUsers() => Ok(await _mediator.Send(new GetUsersQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id) => Ok(await _mediator.Send(new GetUserByIdQuery { Id = id }));

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command) => Ok(await _mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command) => Ok(await _mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id) => Ok(await _mediator.Send(new DeleteUserCommand { Id = id }));
    }
}
