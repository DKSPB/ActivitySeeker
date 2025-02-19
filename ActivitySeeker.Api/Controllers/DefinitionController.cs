using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/definition")]
    public class DefinitionController : ControllerBase
    {
        private readonly IDefinitionService _definitionService;

        public DefinitionController(IDefinitionService definitionService)
        {
            _definitionService = definitionService;
        }

        [HttpPost("create/state")]
        public async Task<IActionResult> CreateState([FromBody] BotState state)
        {
            await _definitionService.CreateState(state);
            return Ok();
        }

        [HttpPost("create/transition")]
        public async Task<IActionResult> CreateTransition([FromBody] BotTransition transition)
        {
            await _definitionService.CreateTransition(transition);
            return Ok();
        }
    }
}
