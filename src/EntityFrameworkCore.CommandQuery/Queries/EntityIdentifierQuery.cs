using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    public class EntityIdentifierQuery<TKey, TReadModel> : PrincipalQueryBase<TReadModel>
    {
        public EntityIdentifierQuery(TKey id, IPrincipal principal)
            : base(principal)
        {
            Id = id;
        }

        public TKey Id { get; set; }
    }
}
