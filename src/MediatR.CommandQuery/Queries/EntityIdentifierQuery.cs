using System.Security.Principal;

namespace MediatR.CommandQuery.Queries
{
    public class EntityIdentifierQuery<TKey, TReadModel> : CacheableQueryBase<TReadModel>
    {

        public EntityIdentifierQuery(IPrincipal principal, TKey id)
            : base(principal)
        {
            Id = id;
        }

        public TKey Id { get; }


        public override string GetCacheKey()
        {
            return $"{typeof(TReadModel).FullName}-{Id}";
        }
    }
}
