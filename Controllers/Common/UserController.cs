using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UseCases.User.Commands.Create;

namespace Controllers.Common
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById()
        {
            return Ok();
        }

        [HttpGet("{id:guid}/activities")]
        public async Task<IActionResult> GetUsersActivities(long id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
        {
            await _mediator.Send(createUserCommand);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }
    }
}
