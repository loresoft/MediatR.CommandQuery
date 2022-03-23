
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
}
