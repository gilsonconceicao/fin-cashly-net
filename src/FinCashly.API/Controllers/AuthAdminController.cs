using FinCashly.Application.Transactions.Queries.GetTransactionList;
using FinCashly.Application.Users.Commands.SetUserRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FinCashly.API.Controllers
{
    [ApiController]
    [Route("api/admin/auth")]
    [Tags("AuthAdmin")]
    public class AuthAdminController : BasePrivateController
    {
        public AuthAdminController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Definir uma permissão para um usuário
        /// </summary>
        [HttpPost("set-role")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> SetRole([FromBody] SetUserRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { Message = "Role atualizada com sucesso!" });
        }

#if DEBUG
        /// <summary>
        /// Executar process de seed (popular banco de dados)
        /// </summary>
        [HttpPost("run-seed")]
        [ProducesResponseType<bool>(StatusCodes.Status200OK)]
        public async Task<IActionResult> RunSeed([FromBody] DatabaseSeederCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
#endif
    }

}

