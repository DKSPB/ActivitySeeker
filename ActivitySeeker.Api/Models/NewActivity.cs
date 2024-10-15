using ActivitySeeker.Bll.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ActivitySeeker.Api.Models;

public class NewActivity
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; }

    public Guid ActivityTypeId { get; set; }

    public string LinkOrDescription { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    
    public IFormFile? Image { get; set; }

    public ActivityDto ToActivityDto()
    {
        return new ActivityDto()
        {
            Id = Id,
            ActivityTypeId = ActivityTypeId,
            StartDate = StartDate,
            LinkOrDescription = LinkOrDescription,
            Image = ImageToByteArray()
        };
    }

    private byte[]? ImageToByteArray()
    {
        if (Image is null)
        {
            return null;
        }

        byte[]? imageData = null;
        using BinaryReader binaryReader = new(Image.OpenReadStream());
        imageData = binaryReader.ReadBytes((int)Image.Length);

        return imageData;
    }
}

public class NewActivityValidator : AbstractValidator<NewActivity>
{
    public NewActivityValidator()
    {
        RuleFor(newActivity => newActivity.ActivityTypeId)
            .NotEmpty().WithMessage("Поле \"Тип активности\" не должно быть пустым");

        RuleFor(newActivity => newActivity.LinkOrDescription)
            .NotEmpty().WithMessage("Поле \"Описание активности\" не должно быть пустым")
            .NotNull().WithMessage("Поле \"Описание активности\" не должно принимать значение null")
            .MaximumLength(100).WithMessage("Максимальная длина описания не должна превышать 1024 символа");

        RuleFor(newActivity => newActivity.StartDate)
            .NotEmpty().WithMessage($"Поле \"Дата начала\" не должно быть пустым")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Дата начала не должно быть раньше текущей даты");
    }
}
