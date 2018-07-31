using System;

namespace EntityFrameworkCore.CommandQuery.Definitions
{
    public interface IHaveIdentifier<TKey>
    {
        TKey Id { get; set; }
    }
}