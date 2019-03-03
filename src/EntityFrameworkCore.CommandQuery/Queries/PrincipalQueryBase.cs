using System;
using System.Security.Principal;
using MediatR;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    public abstract class PrincipalQueryBase<TResponse> : IRequest<TResponse>
    {
        protected PrincipalQueryBase(IPrincipal principal)
        {
            Principal = principal;
        }

        public IPrincipal Principal { get; set; }
    }
}