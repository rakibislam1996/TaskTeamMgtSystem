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

        // Manager: create/update tasks
        [HttpPost]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> CreateTask(CreateTaskItemCommand command)
        {
            var result = await _mediator.Send(command);
            _logger.LogInformation("Task created: {@Command}", command);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> UpdateTask(UpdateTaskItemCommand command)
        {
            var result = await _mediator.Send(command);
            _logger.LogInformation("Task updated: {@Command}", command);
            return Ok(result);
        }

        // Employee: view/update only their own assigned tasks
        [HttpGet]
        [Authorize(Policy = "Employee")]
        public async Task<IActionResult> GetTasks([FromQuery] GetTaskItemsQuery query)
        {
            // Filter tasks to only show tasks assigned to the current user
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            query.AssignedToUserId = userId;
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Employee")]
        public async Task<IActionResult> GetTask(int id) => Ok(await _mediator.Send(new GetTaskItemByIdQuery { Id = id }));

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

        // Manager: delete tasks
        [HttpDelete("{id}")]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _mediator.Send(new DeleteTaskItemCommand { Id = id });
            _logger.LogInformation("Task deleted: {TaskId}", id);
            return Ok(result);
        }
    }
}
