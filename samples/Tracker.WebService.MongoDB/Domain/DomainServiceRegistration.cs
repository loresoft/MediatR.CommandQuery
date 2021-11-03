using System.Collections.Generic;

using KickStart.DependencyInjection;

using MediatR.CommandQuery.MongoDB;

using Microsoft.Extensions.DependencyInjection;

namespace Tracker.WebService.Domain
{
    public class DomainServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddMediator();
            services.AddAutoMapper(typeof(DomainServiceRegistration).Assembly);
            services.AddValidatorsFromAssembly<DomainServiceRegistration>();
        }
    }
}
