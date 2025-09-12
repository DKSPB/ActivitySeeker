namespace UseCases.Activity.Commands.Create
{
    using Models;
    using MediatR;
    public class CreateActivityCommand : IRequest<ActivityDto>
    {
        public Guid ActivityTypeId { get; init; }

        public long UserId { get; set; }
        public string Description { get; init; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Timezone { get; init; }
        public bool IsOnline { get; init; }
        public int? CityId { get; init; }
    }
}

