using System;
using EntityFrameworkCore.CommandQuery.Definitions;

namespace EntityFrameworkCore.CommandQuery.Models
{
    public class EntityIdentifierModel<TKey> : IHaveIdentifier<TKey>
    {
        public TKey Id { get; set; }
    }
}