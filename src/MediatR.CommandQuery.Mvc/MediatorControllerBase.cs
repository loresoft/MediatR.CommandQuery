
using MediatR.CommandQuery.Results;

using Microsoft.AspNetCore.Mvc;

namespace MediatR.CommandQuery.Mvc;

[ApiController]
[Route("api/[controller]")]
public abstract class MediatorControllerBase : ControllerBase
{
    protected MediatorControllerBase(IMediator mediator)
    {
        Mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
    }

    public IMediator Mediator { get; }

    protected virtual ActionResult<T> Result<T>(IResult<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        Response.ContentType = "application/problem+json";

        var problemDetails = result.Error.Problem();
        return StatusCode(result.Error.Status, problemDetails);
    }
}
