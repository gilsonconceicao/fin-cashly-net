using Api.Controllers;
using FinCashly.Application.Transactions.Commands.CreateTransaction;
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
    public async Task<IActionResult> GetUsers([FromQuery] GetTransactionListQuery mediator)
    {
        var users = await _mediator.Send(mediator);
        return Ok(users);
    }

    /// <summary>
    /// Adiciona uma nova transação
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTransactionDto model)
    {
        var create = await _mediator.Send(new CreateTransactionCommand
        {
            Payload = model
        });
        return Ok(create);
    }
}