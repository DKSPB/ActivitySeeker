using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UseCases.User.Commands.Create;
using UseCases.User.Commands.EnsureUserExists;
using UseCases.User.Queries.GetById;

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

        [HttpGet("{userId:long}/exists")]
        public async Task<IActionResult> EnsureUserExists(long userId)
        {
            await _mediator.Send(new EnsureUserExistsCommand(userId));
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet("{userId:long}")]
        public async Task<IActionResult> GetById(long userId)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(userId)));
        }

        [HttpGet("{userId:long}/activities")]
        public async Task<IActionResult> GetUsersActivities(long userId)
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
