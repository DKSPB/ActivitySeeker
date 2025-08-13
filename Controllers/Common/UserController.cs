using MediatR;
using UseCases.User.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UseCases.User.Queries.GetAll;
using UseCases.User.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using UseCases.User.Commands.EnsureUserExists;

namespace Controllers.Common
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        /// <summary>
        /// Создаёт экземпляр контроллера для управления пользователями.
        /// </summary>
        /// <param name="mediator">MediatR для отправки команд и запросов.</param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Проверяет наличие пользователя (или группы) в системе.  
        /// Если пользователь отсутствует, создаёт новую запись.
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя или группы (например, из VK).</param>
        /// <returns>
        /// Если пользователь создан — возвращает 201 (Created) с данными пользователя.  
        /// Если пользователь уже существует — возвращает 200 (OK) с данными пользователя.
        /// </returns>
        /// <response code="200">Пользователь найден и возвращён.</response>
        /// <response code="201">Пользователь создан и возвращён.</response>
        [HttpPost("{userId:long}/exists")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> EnsureUserExists(long userId)
        {
            var (isCreated, user) = await _mediator.Send(new EnsureUserExistsCommand(userId));

            return isCreated ? CreatedAtAction(nameof(GetById), new { userId = user.Id }, user) : Ok(user);
        }

        /// <summary>
        /// Получает список всех пользователей с поддержкой пагинации.
        /// </summary>
        /// <param name="limit">Количество записей, возвращаемых за один запрос (по умолчанию 20).</param>
        /// <param name="offset">Смещение относительно начала выборки (по умолчанию 1).</param>
        /// <returns>Коллекция объектов <see cref="UserDto"/>.</returns>
        /// <response code="200">Список пользователей успешно получен.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int limit = 20, int offset = 1)
        {
            return Ok(await _mediator.Send(new GetUsersQuery(limit, offset)));
        }

        /// <summary>
        /// Получает данные пользователя (или группы) по его идентификатору.
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя или группы (например, из VK).</param>
        /// <returns>Объект <see cref="UserDto"/>.</returns>
        /// <response code="200">Пользователь найден и возвращён.</response>
        /// <response code="404">Пользователь с указанным идентификатором не найден.</response>
        [HttpGet("{userId:long}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long userId)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(userId)));
        }
    }
}
