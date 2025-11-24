using Api.Controllers;
using FinCashly.Application.Categories.Commands.CreateCategory;
using FinCashly.Application.Categories.Commands.DeleteCategory;
using FinCashly.Application.Categories.Commands.UpdateCategory;
using FinCashly.Application.Categories.Queries.GetCategoryList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Categories")]
public class CategoryController : BaseController
{
    public CategoryController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Obtém todas as categorias.
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     GET /api/category?page=0&amp;size=10
    ///
    /// </remarks>
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListPaginated([FromQuery] GetCategoryQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    /// <summary>
    /// Cria uma nova categoria.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDto model)
    {
        var result = await _mediator.Send(new CreateCategoryCommand { Payload = model });
        return CreatedAtAction(nameof(GetListPaginated), result);
    }

    /// <summary>
    /// Atualiza uma categoria existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCategoryDto model)
    {
        var result = await _mediator.Send(new UpdateCategoryCommand { Id = id, Payload = model });
        return Ok(result);
    }

    /// <summary>
    /// Exclui uma categoria existente.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _mediator.Send(new DeleteCategoryCommand { Id = id });
        return NoContent();
    }
}
