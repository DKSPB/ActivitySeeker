using MediatR;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Commands.Update
{
    public record UpdateActivityTypeCommand : IRequest<ActivityTypeDto>
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
    }
}
