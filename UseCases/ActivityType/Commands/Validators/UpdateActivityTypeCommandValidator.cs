namespace UseCases.ActivityType.Commands.Validators
{
    using Update;
    using FluentValidation;
    using Interfaces.Repos;
    public class UpdateActivityTypeCommandValidator : AbstractValidator<UpdateActivityTypeCommand>
    {
        public UpdateActivityTypeCommandValidator(IActivityTypeRepository repository)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Идентификатор активности не может быть пустым");
        
            RuleFor(x => x.ParentId)
                .Must((model, parentTypeId) => parentTypeId == null || model.Id != parentTypeId)
                .WithMessage("Идентификатор типа не может совпадать с идентификатором родительского типа.");
        
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