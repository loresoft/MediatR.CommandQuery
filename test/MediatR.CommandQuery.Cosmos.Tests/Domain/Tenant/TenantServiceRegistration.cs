using System;
using System.Collections.Generic;
using Cosmos.Abstracts;
using KickStart.DependencyInjection;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace MediatR.CommandQuery.Cosmos.Tests.Domain
{
    public class TenantServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddEntityQueries<ICosmosRepository<Data.Entities.Tenant>, Data.Entities.Tenant, TenantReadModel>();
            services.AddEntityCommands<ICosmosRepository<Data.Entities.Tenant>, Data.Entities.Tenant, TenantReadModel, TenantCreateModel, TenantUpdateModel>();
        }
    }
}
