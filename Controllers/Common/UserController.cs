using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.User.Queries.GetAll;
using UseCases.User.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using UseCases.User.Commands.EnsureUserExists;
using UseCases.User.Queries.GetUsersActivities;



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

        /// <summary>
        /// Метод проверяет наличие пользователя (группы) в системе и создаёт его (её) в противном случае
        /// </summary>
        /// <param name="userId">Идентификатор пользователя (группы)</param>
        /// <returns></returns>
        [HttpGet("{userId:long}/exists")]
        public async Task<IActionResult> EnsureUserExists(long userId)
        {
            await _mediator.Send(new EnsureUserExistsCommand(userId));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int limit = 20, int offset = 1)
        {
            return Ok(await _mediator.Send(new GetUsersQuery(limit, offset)));
        }

        /// <summary>
        /// Получение информации о пользователе (группе) по заданному идентификатору VK
        /// </summary>
        /// <param name="userId">Идентификатор пользователя (группы)</param>
        /// <returns></returns>
        [HttpGet("{userId:long}")]
        public async Task<IActionResult> GetById(long userId)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(userId)));
        }

        /// <summary>
        /// Получение всех активностей заданного пользователя (группы)
        /// </summary>
        /// <param name="userId">Идентификатор пользователя (группы)</param>
        /// <returns></returns>
        [HttpGet("{userId:long}/activities")]
        public async Task<IActionResult> GetUsersActivities(long userId)
        {
            return Ok(await _mediator.Send(new GetUsersActivitiesQuery(userId)));
        }

    }
}
