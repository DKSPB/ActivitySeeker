using ActivitySeeker.Api.Models;
using ActivitySeeker.UseCases.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Api;

[ApiController]
[AllowAnonymous]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly ISender _sender;
    //private readonly IAdminService _adminService;
    
    public AdminController(ISender sender/*IAdminService adminService*/)
    {
        _sender = sender;
        //_adminService = adminService;
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
    }
}