using AutoMapper;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Constants;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority.Models;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Acceptance;

[Collection(DatabaseCollection.CollectionName)]
public class PriorityTests : DatabaseTestBase
{
    public PriorityTests(ITestOutputHelper output, DatabaseFixture databaseFixture)
        : base(output, databaseFixture)
    {
    }

    [Fact]
    [Trait("Category", "SqlServer")]
    public async Task EntityIdentifierQuery()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var identifierQuery = new EntityIdentifierQuery<Guid, PriorityReadModel>(MockPrincipal.Default, PriorityConstants.Normal);
        var identifierResult = await mediator.Send(identifierQuery);
        identifierResult.Should().NotBeNull();
        identifierResult.Value.Id.Should().Be(PriorityConstants.Normal);
    }

    [Fact]
    [Trait("Category", "SqlServer")]
    public async Task EntityIdentifiersQuery()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var identifiers = new[]
        {
            PriorityConstants.Normal,
            PriorityConstants.High
        };

        var identifierQuery = new EntityIdentifiersQuery<Guid, PriorityReadModel>(MockPrincipal.Default, identifiers);
        var identifierResults = await mediator.Send(identifierQuery);

        identifierResults.Should().NotBeNull();
        identifierResults.Value.Count.Should().Be(2);
    }

    [Fact]
    [Trait("Category", "SqlServer")]
    public async Task EntityQueryIn()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var identifiers = new[]
        {
            PriorityConstants.Normal,
            PriorityConstants.High
        };

        // Query Entity
        var entityQuery = new EntityQuery
        {
            Sort = new List<EntitySort> { new EntitySort { Name = "Updated", Direction = "Descending" } },
            Filter = new EntityFilter { Name = "Id", Operator = "in", Value = identifiers }
        };
        var listQuery = new EntityPagedQuery<PriorityReadModel>(MockPrincipal.Default, entityQuery);

        var listResult = await mediator.Send(listQuery);
        listResult.Should().NotBeNull();
        listResult.Value.Total.Should().Be(2);
    }

    [Fact]
    [Trait("Category", "SqlServer")]
    public async Task EntityQueryDescriptionNull()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        // Query Entity
        var entityQuery = new EntityQuery
        {
            Sort = new List<EntitySort> { new EntitySort { Name = "Updated", Direction = "Descending" } },
            Filter = new EntityFilter { Name = "Description", Operator = "IsNull" }
        };
        var listQuery = new EntityPagedQuery<PriorityReadModel>(MockPrincipal.Default, entityQuery);

        var listResult = await mediator.Send(listQuery);
        listResult.Should().NotBeNull();
    }

    [Fact]
    [Trait("Category", "SqlServer")]
    public async Task EntityQueryDescriptionNOtNull()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        // Query Entity
        var entityQuery = new EntityQuery
        {
            Sort = new List<EntitySort> { new EntitySort { Name = "Updated", Direction = "Descending" } },
            Filter = new EntityFilter { Name = "Description", Operator = "is not null" }
        };
        var listQuery = new EntityPagedQuery<PriorityReadModel>(MockPrincipal.Default, entityQuery);

        var listResult = await mediator.Send(listQuery);
        listResult.Should().NotBeNull();
    }


    [Fact]
    [Trait("Category", "SqlServer")]
    public async Task EntityQueryMultipleFilters()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        // Query Entity
        var entityQuery = new EntityQuery
        {
            Sort = new List<EntitySort> { new EntitySort { Name = "Updated", Direction = "Descending" } },
            Filter = new EntityFilter
            {
                Filters = new List<EntityFilter> {
                    new EntityFilter { Name = "Description", Operator = "is null" },
                    new EntityFilter { Name = "Name", Operator = "equals", Value = "High" }
                }
            }
        };
        var listQuery = new EntityPagedQuery<PriorityReadModel>(MockPrincipal.Default, entityQuery);

        var listResult = await mediator.Send(listQuery);
        listResult.Should().NotBeNull();
    }
}
