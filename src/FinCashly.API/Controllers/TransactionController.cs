using Api.Controllers;
using FinCashly.Application.Transactions.Commands.CreateTransaction;
using FinCashly.Application.Transactions.Commands.DeleteTransaction;
using FinCashly.Application.Transactions.Commands.UpdateTransaction;
using FinCashly.Application.Transactions.Queries.GetTransactionList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers;

public class TransactionController : BaseController
{
    public TransactionController(IMediator mediator) : base(mediator)
    {
    }
    /// <summary>
    /// Obtem todas as transações
    /// </summary>
    /// <param name="mediator">Parâmetros de entrada</param>
    [HttpGet]
    public async Task<IActionResult> GetListPaginated([FromQuery] GetTransactionListQuery mediator)
    {
        var users = await _mediator.Send(mediator);
        return Ok(users);
    }

    /// <summary>
    /// Adiciona uma nova transação em uma conta
    /// </summary>
    [HttpPost("{accountId}")]
    public async Task<IActionResult> CreateAsync([FromRoute] Guid accountId, [FromBody] CreateTransactionDto model)
    {
        var create = await _mediator.Send(new CreateTransactionCommand
        {
            AccountId = accountId,
            Payload = model
        });
        return Ok(create);
    }

    /// <summary>
    /// Atualiza uma transação
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateTransactionDto model)
    {
        var Update = await _mediator.Send(new UpdateTransactionCommand
        {
            Id = id,
            Payload = model
        });
        return Ok(Update);
    }

    /// <summary>
    /// Remove uma transação existente
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteTransactionCommand
        {
            Id = id
        });
        return NoContent();
    }
}