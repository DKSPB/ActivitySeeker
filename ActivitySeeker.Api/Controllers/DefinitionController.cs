using ActivitySeeker.Api.Models.DefinitionModels;
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

        [HttpGet("get/state")]
        public async Task<IEnumerable<StateViewModel>> GetStates()
        {
            return (await _definitionService.GetStates()).Select(x => new StateViewModel(x));
        }

        [HttpPost("create/state")]
        public async Task<IActionResult> CreateState([FromBody] CreateUpdateStateModel state)
        {
            await _definitionService.CreateState(state.ToEntity());
            return Ok();
        }

        [HttpPut("update/state")]
        public async Task<IActionResult> UpdateState([FromBody] CreateUpdateStateModel state)
        {
            await _definitionService.UpdateState(state.ToEntity());
            return Ok();
        }
        

        [HttpGet("get/transitions")]
        public async Task<IEnumerable<TransitionViewModel>> GetTransitions()
        {
            return (await _definitionService.GetTransitions()).Select(x => new TransitionViewModel(x));
        }

        [HttpPost("create/transition")]
        public async Task<IActionResult> CreateTransition([FromBody] CreateUpdateTransitionModel transition)
        {
            await _definitionService.CreateTransition(transition.ToEntity());
            return Ok();
        }

        [HttpPut("update/transition")]
        public async Task<IActionResult> UpdateTransition([FromBody] CreateUpdateTransitionModel transition)
        {
            await _definitionService.UpdateTransition(transition.ToEntity());
            return Ok();
        }
    }
}
