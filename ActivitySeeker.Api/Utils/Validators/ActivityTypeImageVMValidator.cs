using ActivitySeeker.Api.Models;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.Utils.Validators;

public class ActivityTypeImageVValidator : AbstractValidator<ActivityTypeImageVM>
{
    public ActivityTypeImageVValidator(IOptions<BotConfiguration> botConfigOptions)
    {
        var maxFileSize = botConfigOptions.Value.MaxFileSize;
        
        RuleFor(x => x.File)
            .NotNull().WithMessage("Файл обязателен")
            .Must(file => file.Length <= maxFileSize).WithMessage($"Размер файла превышает {maxFileSize / (1024 * 1024)} Мб")
            .Must(file => new[] { ".jpg", ".png"}
                .Contains(Path.GetExtension(file.FileName).ToLower()))
            .WithMessage("Недопустимое расширение файла");
    }
}