using DataAccess.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UseCases.ActivityType.Commands.Update;

namespace UseCases.ActivityType.Commands.Validators;

public class UpdateActivityTypeCommandValidator : AbstractValidator<UpdateActivityTypeCommand>
{
    public UpdateActivityTypeCommandValidator(IDbContext context)
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

                return await context.ActivityTypes
                    .AnyAsync(a => a.Id == parentId.Value, ct);
            })
            .WithMessage("Родительский тип активности не найден");
    }
}