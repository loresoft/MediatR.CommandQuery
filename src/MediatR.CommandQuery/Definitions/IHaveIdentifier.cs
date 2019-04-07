using System;

namespace MediatR.CommandQuery.Definitions
{
    public interface IHaveIdentifier<TKey>
    {
        TKey Id { get; set; }
    }
}