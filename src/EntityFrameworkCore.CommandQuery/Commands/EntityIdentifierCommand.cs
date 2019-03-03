using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public abstract class EntityIdentifierCommand<TKey, TReadModel>
        : PrincipalCommandBase<TReadModel>
    {
        protected EntityIdentifierCommand(TKey id, IPrincipal principal)
            : base(principal)
        {
            Id = id;
        }

        public TKey Id { get; }
    }
}