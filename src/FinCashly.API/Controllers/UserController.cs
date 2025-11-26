using Api.Controllers;
using FinCashly.Application.Users.Commands.CreateUser;
using FinCashly.Application.Users.Commands.DeleteUser;
using FinCashly.Application.Users.Commands.UpdateUser;
using FinCashly.Application.Users.Queries.GetUsersList;
using FinCashly.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Users")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Obtém uma lista paginada de usuários.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/user?page=0&amp;size=10
        ///
        /// </remarks>
        [HttpGet]
        [ProducesResponseType<Paginated<GetUserPaginatedDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto model)
        {
            var result = await _mediator.Send(new CreateUserCommand { Payload = model });
            return CreatedAtAction(nameof(GetUsers), result);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UpdateUserDto model)
        {
            var result = await _mediator.Send(new UpdateUserCommand { Id = id, Payload = model });
            return Ok(result);
        }

        /// <summary>
        /// Exclui um usuário existente.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand { Id = id });
            return NoContent();
        }
    }
}
