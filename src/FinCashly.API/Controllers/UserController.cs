using System.Threading.Tasks;
using Api.Controllers;
using FinCashly.Application.Common.DTOs;
using FinCashly.Application.Users.Commands.CreateUser;
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
            var firstTeste = await _mediator.Send(mediator);
            return Ok(firstTeste);
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto model)
        {
            var firstTeste = await _mediator.Send(new CreateUserCommand
            {
                Payload = model
            });
            return Ok(firstTeste);
        }
    }
}