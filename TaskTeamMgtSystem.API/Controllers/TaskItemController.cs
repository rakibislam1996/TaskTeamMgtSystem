using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTeamMgtSystem.Application.TaskItems.Commands;
using TaskTeamMgtSystem.Application.TaskItems.Queries;
using TaskTeamMgtSystem.Application.Common.Services;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace TaskTeamMgtSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TaskItemController> _logger;
        private readonly Application.Common.Services.IAuthorizationService _authorizationService;

        public TaskItemController(IMediator mediator, ILogger<TaskItemController> logger, Application.Common.Services.IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        // Admin/Manager: create tasks
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateTask(CreateTaskItemCommand command)
        {
            var result = await _mediator.Send(command);
            _logger.LogInformation("Task created: {@Command}", command);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateTask(UpdateTaskItemCommand command)
        {
            var result = await _mediator.Send(command);
            _logger.LogInformation("Task updated: {@Command}", command);
            return Ok(result);
        }

        // All authenticated users can view tasks (with filtering based on role)
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTasks([FromQuery] GetTaskItemsQuery query)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
            
            // Admin and Manager can see all tasks
            // Employee can only see tasks assigned to them
            if (userRole == "Employee")
            {
                query.AssignedToUserId = userId;
            }
            
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTask(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
            
            // Admin and Manager can access any task
            // Employee can only access tasks assigned to them
            if (userRole == "Employee")
            {
                if (!await _authorizationService.CanUserAccessTaskAsync(userId, id))
                {
                    return Forbid();
                }
            }
            
            return Ok(await _mediator.Send(new GetTaskItemByIdQuery { Id = id }));
        }

        [HttpPut("employee/update")]
        [Authorize(Policy = "Employee")]
        public async Task<IActionResult> UpdateOwnTask(UpdateTaskItemCommand command)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            // Verify user can only update their own assigned tasks
            if (!await _authorizationService.CanUserAccessTaskAsync(userId, command.Id))
            {
                return Forbid();
            }

            var result = await _mediator.Send(command);
            _logger.LogInformation("Employee updated own task: {@Command}", command);
            return Ok(result);
        }

        // Admin/Manager: delete tasks
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _mediator.Send(new DeleteTaskItemCommand { Id = id });
            _logger.LogInformation("Task deleted: {TaskId}", id);
            return Ok(result);
        }
    }
}
