using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MediatR.CommandQuery.Endpoints;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithEntityMetadata(this RouteHandlerBuilder builder, string entityName)
    {
        return builder
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithTags(entityName);
    }

}
