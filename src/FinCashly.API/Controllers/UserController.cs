using System.Threading.Tasks;
using Api.Controllers;
using FinCashly.Application.Users.GetUsersList;
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

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersListQuery mediator)
        {
            var firstTeste = await _mediator.Send(mediator);
            return Ok(firstTeste);
        }
    }
}