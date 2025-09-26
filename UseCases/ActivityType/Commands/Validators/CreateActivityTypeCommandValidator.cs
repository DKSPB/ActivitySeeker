namespace UseCases.ActivityType.Commands.Validators
{
    using Create;
    using FluentValidation;
    using Interfaces.Repos;
    public class CreateActivityTypeCommandValidator : AbstractValidator<CreateActivityTypeCommand>
    {
        public CreateActivityTypeCommandValidator(IActivityTypeRepository repository)
        {
            RuleFor(x => x.TypeName).MaximumLength(50)
                .WithMessage("Название типа активности не должно быть больше 50 символов");

            RuleFor(x => x.ParentId)
                .MustAsync(async (parentId, ct) =>
                {
                    if (parentId == null)
                        return true;

                    return await repository.AnyAsync(parentId.Value, ct);
                })
                .WithMessage("Родительский тип активности не найден");
        }
    }
}