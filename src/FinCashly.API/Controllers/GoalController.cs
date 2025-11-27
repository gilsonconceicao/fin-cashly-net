using FinCashly.Application.Goals.Commands.CreateGoal;
using FinCashly.Application.Goals.Commands.DeleteGoal;
using FinCashly.Application.Goals.Commands.UpdateGoal;
using FinCashly.Application.Goals.Queries.GetGoalList;
using FinCashly.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Goals")]
public class GoalController : BasePublicController
{
    public GoalController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Obtém todas as metas.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     GET /api/goal?page=0&amp;size=10
    ///
    /// </remarks>
    [HttpGet]
    [ProducesResponseType<Paginated<GetGoalPaginatedDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListPaginated([FromQuery] GetGoalPaginatedListQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    /// <summary>
    /// Cria uma nova meta.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateGoalDto model)
    {
        var result = await _mediator.Send(new CreateGoalCommand { Payload = model });
        return Ok(result);
    }

    /// <summary>
    /// Atualiza uma meta existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateGoalDto model)
    {
        var result = await _mediator.Send(new UpdateGoalCommand { Id = id, Payload = model });
        return Ok(result);
    }

    /// <summary>
    /// Exclui uma meta existente.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _mediator.Send(new DeleteGoalCommand { Id = id });
        return NoContent();
    }
}
