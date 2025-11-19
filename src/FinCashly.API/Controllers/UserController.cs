using Api.Controllers;
using FinCashly.Application.Users.Commands.CreateUser;
using FinCashly.Application.Users.Commands.DeleteUser;
using FinCashly.Application.Users.Commands.UpdateUser;
using FinCashly.Application.Users.Queries.GetUsersList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator)
             : base(mediator)
        {
        }

        /// <summary>
        /// Obtem todos os usuários
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersListQuery mediator)
        {
            var users = await _mediator.Send(mediator);
            return Ok(users);
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto model)
        {
            var create = await _mediator.Send(new CreateUserCommand
            {
                Payload = model
            });
            return Ok(create);
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, UpdateUserDto model)
        {
            var update = await _mediator.Send(new UpdateUserCommand
            {
                Id = id,
                Payload = model
            });
            return Ok(update);
        }

        /// <summary>
        /// Remove um usuário existente
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
        {
            var update = await _mediator.Send(new DeleteUserCommand
            {
                Id = id
            });
            return Ok(update);
        }
    }
}