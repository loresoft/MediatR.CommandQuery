using Injectio.Attributes;

using MediatR.CommandQuery.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;

namespace Tracker.WebService.Domain;

public class DomainServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddMediator();
        services.AddAutoMapper(typeof(DomainServiceRegistration).Assembly);
        services.AddValidatorsFromAssembly<DomainServiceRegistration>();
    }
}
