using System;
using System.Security.Principal;
using MediatR;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public abstract class PrincipalCommandBase<TResponse> : IRequest<TResponse>
    {
        protected PrincipalCommandBase(IPrincipal principal)
        {
            Principal = principal;
        }

        public IPrincipal Principal { get; }
    }
}