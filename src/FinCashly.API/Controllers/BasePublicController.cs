// using MediatR;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BasePublicController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BasePublicController(IMediator mediator)
    {
        _mediator = mediator;
    }
}