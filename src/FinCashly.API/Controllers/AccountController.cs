using Api.Controllers;
using FinCashly.Application.Accounts.Commands.CreateAccount;
using FinCashly.Application.Accounts.Commands.DeleteAccount;
using FinCashly.Application.Accounts.Commands.UpdateAccount;
using FinCashly.Application.Accounts.Queries.GetAccountsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Accounts")]
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Obtém uma lista paginada de contas.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/account?page=0&amp;size=10
        ///
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccounts([FromQuery] GetAccountsListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Cria uma nova conta para um usuário.
        /// </summary>
        [HttpPost("{userId:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(Guid userId, [FromBody] CreateAccountDto model)
        {
            var result = await _mediator.Send(new CreateAccountCommand
            {
                UserId = userId,
                Payload = model
            });

            return CreatedAtAction(nameof(GetAccounts), new { userId }, result);
        }

        /// <summary>
        /// Atualiza uma conta existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateAccountDto model)
        {
            var result = await _mediator.Send(new UpdateAccountCommand
            {
                AccountId = id,
                Payload = model
            });

            return Ok(result);
        }

        /// <summary>
        /// Remove uma conta existente.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteAccountCommand { Id = id });
            return NoContent();
        }
    }
}
