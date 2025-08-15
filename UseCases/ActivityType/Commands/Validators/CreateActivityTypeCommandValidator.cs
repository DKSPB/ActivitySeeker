using DataAccess.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UseCases.ActivityType.Commands.Create;

namespace UseCases.ActivityType.Commands.Validators;

public class CreateActivityTypeCommandValidator : AbstractValidator<CreateActivityTypeCommand>
{
    public CreateActivityTypeCommandValidator(IDbContext context)
    {
        RuleFor(x => x.TypeName).MaximumLength(50)
            .WithMessage("Название типа активности не должно быть больше 50 символов");

        RuleFor(x => x.ParentId)
            .MustAsync(async (parentId, ct) =>
            {
                if (parentId == null)
                    return true;

                return await context.ActivityTypes
                    .AnyAsync(a => a.Id == parentId.Value, ct);
            })
            .WithMessage("Родительский тип активности не найден");
    }
}