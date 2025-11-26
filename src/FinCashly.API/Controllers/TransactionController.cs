using Api.Controllers;
using FinCashly.Application.Transactions.Commands.CreateTransaction;
using FinCashly.Application.Transactions.Commands.DeleteTransaction;
using FinCashly.Application.Transactions.Commands.UpdateTransaction;
using FinCashly.Application.Transactions.Queries.GetTransactionList;
using FinCashly.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Transactions")]
public class TransactionController : BaseController
{
    public TransactionController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Obtém uma lista paginada de transações.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     GET /api/transaction?page=0&amp;size=10
    ///
    /// </remarks>
    [HttpGet]
    [ProducesResponseType<Paginated<GetTransactionPaginatedDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListPaginated([FromQuery] GetTransactionListQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    /// <summary>
    /// Cria uma nova transação vinculada a uma conta.
    /// </summary>
    [HttpPost("{accountId:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync(Guid accountId, [FromBody] CreateTransactionDto model)
    {
        var result = await _mediator.Send(new CreateTransactionCommand
        {
            AccountId = accountId,
            Payload = model
        });

        return CreatedAtAction(nameof(GetListPaginated), result);
    }

    /// <summary>
    /// Atualiza uma transação existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateTransactionDto model)
    {
        var result = await _mediator.Send(new UpdateTransactionCommand { Id = id, Payload = model });
        return Ok(result);
    }

    /// <summary>
    /// Exclui uma transação existente.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _mediator.Send(new DeleteTransactionCommand { Id = id });
        return NoContent();
    }
}
