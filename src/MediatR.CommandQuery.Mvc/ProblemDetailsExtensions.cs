using MediatR.CommandQuery.Results;

using Microsoft.AspNetCore.Mvc;

namespace MediatR.CommandQuery.Mvc;

public static class ProblemDetailsExtensions
{
    public static ProblemDetails Problem(this IError error)
    {
        var problem = new ProblemDetails
        {
            Status = error.Status,
            Title = error.Message
        };

        if (error.Extensions == null || error.Extensions.Count > 0)
            return problem;

        foreach (var extension in error.Extensions)
            problem.Extensions[extension.Key] = extension.Value;

        return problem;
    }
}
