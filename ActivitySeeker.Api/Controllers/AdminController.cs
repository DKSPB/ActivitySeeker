using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[Route("api/admin")]
[AllowAnonymous]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    
    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterAdmin newAdmin)
    {
        await _adminService.RegisterAsync(newAdmin.Username, newAdmin.Login, newAdmin.Password);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> Login([FromQuery] string username, [FromQuery] string password)
    {
        var token  = await _adminService.LoginAsync(username, password);
        return Ok(token);
    }
}