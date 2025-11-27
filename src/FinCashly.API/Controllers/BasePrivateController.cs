// using MediatR;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class BasePrivateController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BasePrivateController(IMediator mediator)
    {
        _mediator = mediator;
    }
}