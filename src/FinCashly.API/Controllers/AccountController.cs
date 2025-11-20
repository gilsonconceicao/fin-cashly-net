using Api.Controllers;
using FinCashly.Application.Accounts.Commands.CreateAccount;
using FinCashly.Application.Accounts.Commands.DeleteAccount;
using FinCashly.Application.Accounts.Commands.UpdateAccount;
using FinCashly.Application.Accounts.Queries.GetAccountsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator)
             : base(mediator)
        {
        }

        /// <summary>
        /// Obtem todos os contas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery] GetAccountsListQuery mediator)
        {
            return Ok(await _mediator.Send(mediator));
        }

        /// <summary>
        /// Adiciona uma nova conta a um usuário
        /// </summary>
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateAsync([FromRoute] Guid userId, [FromBody] CreateAccountDto model)
        {
            var create = await _mediator.Send(new CreateAccountCommand
            {
                UserId = userId,
                Payload = model
            });
            return Ok(create);
        }

        /// <summary>
        /// Atualiza uma conta existente de um usuário
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateAccountDto model)
        {
            var update = await _mediator.Send(new UpdateAccountCommand
            {
                AccountId = id,
                Payload = model
            });
            return Ok(update);
        }

        /// <summary>
        /// Remove uma conta existente
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var update = await _mediator.Send(new DeleteAccountCommand
            {
                Id = id
            });
            return Ok(update);
        }
    }
}