using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Controllers.Admin;

[ApiController]
[AllowAnonymous]
[Route("api/admin")]
public class AdminController : ControllerBase
{
   // private readonly IAdminService _adminService;
    
    /*public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterAdmin newAdmin)
    {
        await _adminService.RegisterAsync(newAdmin.Login, newAdmin.Password);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> Login([FromQuery] string login, [FromQuery] string password)
    {
        var token  = await _adminService.LoginAsync(login, password);
        return Ok(token);
    }*/
}