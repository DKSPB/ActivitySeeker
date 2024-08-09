using ActivitySeeker.Bll.Models;
using FluentValidation;

namespace ActivitySeeker.Api.Models;

public class NewActivity
{
    public Guid ActivityTypeId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public string? Link { get; set; }
    
    public IFormFile? Image { get; set; }

    public ActivityDto ToActivityDto()
    {
        return new ActivityDto()
        {
            ActivityTypeId = ActivityTypeId,
            StartDate = StartDate,
            Name = Name,
            Description = Description,
            Link = Link,
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
        
        RuleFor(newActivity => newActivity.Name)
            .NotEmpty().WithMessage("Поле \"Название активности\" не должно быть пустым")
            .NotNull().WithMessage("Поле \"Название активности\" не должно принимать значение null")
            .MaximumLength(100).WithMessage("Максимальная длина названия не должна превышать 100 символов");

        RuleFor(newActivity => newActivity.Description)
            .NotEmpty().WithMessage("Поле \"Описание активности\" не должно быть пустым")
            .NotNull().WithMessage("Поле \"Описание активности\" не должно принимать значение null")
            .MaximumLength(100).WithMessage("Максимальная длина описания не должна превышать 1024 символа");

        RuleFor(newActivity => newActivity.StartDate)
            .NotEmpty().WithMessage($"Поле \"Дата начала\" не должно быть пустым")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Дата начала не должно быть раньше текущей даты");

        RuleFor(newActivity => newActivity.Link)
            .NotEmpty().When(newActivity => newActivity.Image is null)
            .WithMessage("Одно из полей: ссылка на активность или изображение должно быть заполнено");

        RuleFor(newActivity => newActivity.Image)
            .NotEmpty().When(newActivity => newActivity.Link is null)
            .WithMessage("Одно из полей: ссылка на активность или изображение должно быть заполнено");
        //.Must(newActivity => newActivity?.ContentType is "image/jpeg" or "image/png")
        //.WithMessage("Загружаемый файл должен иметь расширение .jpg или .png");
    }
}
