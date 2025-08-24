using MediatR;
using System;

namespace TaskTeamMgtSystem.Application.TaskItems.Commands
{
    public class CreateTaskItemCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedToUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
    }
}