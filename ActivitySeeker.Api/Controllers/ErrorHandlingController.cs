using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers;

/// <summary>
/// Контроллер обработки ошибок
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("ErrorHandling")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorHandlingController : Controller
{
    private readonly IHostEnvironment _hostEnvironment;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="hostEnvironment"></param>
    public ErrorHandlingController(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }
    
    /// <summary>
    /// Метод отображения ошибок
    /// </summary>
    /// <returns></returns>
    [Route("ProcessError")]
    public IActionResult ProcessError()
    {
        var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        if (_hostEnvironment.IsDevelopment())
        {
            return Problem(
                title: feature.Error.Message,
                detail: feature.Error.StackTrace
            );
        }
        
        return Problem();
    }
}