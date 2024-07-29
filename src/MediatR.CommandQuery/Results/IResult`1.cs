using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatR.CommandQuery.Results;

/// <summary>Defines a result with a value.</summary>
public interface IResult<out TValue> : IResult
{
    /// <summary>Gets the value of the result, throwing an exception if the result is failed.</summary>
    /// <returns>The value of the result.</returns>
    /// <exception cref="InvalidOperationException">Thrown when attempting to get or set the value of a failed result.</exception>
    public TValue Value { get; }
}
