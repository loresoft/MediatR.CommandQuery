using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Models
{
    public class EntityIdentifierModel<TKey> : IHaveIdentifier<TKey>
    {
        public TKey Id { get; set; }
    }
}