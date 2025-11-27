using FinCashly.API.Controllers;
using FinCashly.Application.Users.Commands.SetUserRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin/auth")]
public class AuthAdminController : BasePrivateController
{

    public AuthAdminController(IMediator mediator) : base(mediator) { }


    [Authorize(Roles = "Admin")]
    [HttpPost("set-role")]
    public async Task<IActionResult> SetRole([FromBody] SetUserRoleCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { Message = "Role atualizada com sucesso!" });
    }
}
