using ActivitySeeker.Bll.Models;
using FluentValidation;

namespace ActivitySeeker.Api.Models;

public class CreateUpdateActivityViewModel
{
    public Guid Id { get; set; }

    public Guid ActivityTypeId { get; set; }

    public string? Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public string? Link { get; set; }
    
    public FormFileCollection? Images { get; set; }

    public ActivityDto ToActivityDto()
    {
        return new ActivityDto()
        {
            Id = Id,
            ActivityTypeId = ActivityTypeId,
            StartDate = StartDate,
            Description = Description,
            Link = Link,
            Images = ToImageDto()
        };
    }

    private IEnumerable<ImageDto>? ToImageDto()
    {
        if (Images is null || Images.Count == 0)
        {
            return null;
        }

        List<ImageDto> images = new();
        
        foreach (var image in Images)
        {
            byte[]? imageData = null;
            using BinaryReader binaryReader = new(image.OpenReadStream());
            imageData = binaryReader.ReadBytes((int)image.Length);
            images.Add(new ImageDto
            {
                Content = imageData,
            });
        }
        
        return images;
    }
}

public class NewActivityValidator : AbstractValidator<CreateUpdateActivityViewModel>
{
    public NewActivityValidator()
    {
        RuleFor(newActivity => newActivity.ActivityTypeId)
            .NotEmpty().WithMessage("Поле \"Тип активности\" не должно быть пустым");

        RuleFor(newActivity => newActivity.Description)
            .NotEmpty().WithMessage("Поле \"Описание активности\" не должно быть пустым")
            .NotNull().WithMessage("Поле \"Описание активности\" не должно принимать значение null")
            .MaximumLength(100).WithMessage("Максимальная длина описания не должна превышать 1024 символа");

        RuleFor(newActivity => newActivity.StartDate)
            .NotEmpty().WithMessage($"Поле \"Дата начала\" не должно быть пустым")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Дата начала не должно быть раньше текущей даты");

        //RuleFor(newActivity => newActivity.Link)
        //    .NotEmpty().When(newActivity => newActivity.Image is null)
        //    .WithMessage("Одно из полей: ссылка на активность или изображение должно быть заполнено");

        //RuleFor(newActivity => newActivity.Image)
        //    .NotEmpty().When(newActivity => newActivity.Link is null)
        //    .WithMessage("Одно из полей: ссылка на активность или изображение должно быть заполнено");
        //.Must(newActivity => newActivity?.ContentType is "image/jpeg" or "image/png")
        //.WithMessage("Загружаемый файл должен иметь расширение .jpg или .png");
    }
}
