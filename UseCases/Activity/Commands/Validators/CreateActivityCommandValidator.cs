namespace UseCases.Activity.Commands.Validators
{
    using Create;
    using FluentValidation;
    using Interfaces.Repos;
    public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityCommandValidator(IActivityTypeRepository repository)
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Необходимо заполнить описание (максимум 2000 символов)")
                .MaximumLength(2000)
                .WithMessage("Описание не должно быть длинее 2000 символов");

            RuleFor(x => x.Timezone)
                .GreaterThanOrEqualTo(-12)
                .WithMessage("Часовой пояс не может быть меньше -12 ")
                .LessThanOrEqualTo(12)
                .WithMessage("Часовой пояс не может быть боьлше +12");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Дата начала активности не может быть пустой")
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Дата начала активности должна равняться или быть больше текущей");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .When(x => x.EndDate.HasValue)
                .WithMessage("Дата завершения активности должна быть позже даты начала");

            RuleFor(x => x.ActivityTypeId)
                .MustAsync(async (activityTypeId, ct) => await repository.AnyAsync(activityTypeId, ct))
                .WithMessage("Тип активности с идентификатором '{PropertyValue}' не существует");
        }
    }
}