using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using MediatR.CommandQuery.MongoDB.Tests.Constants;
using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.DependencyInjection;

using Xunit.Abstractions;

namespace MediatR.CommandQuery.MongoDB.Tests.Acceptance;

[Collection(DatabaseCollection.CollectionName)]
public class PriorityTests : DatabaseTestBase
{
    public PriorityTests(ITestOutputHelper output, DatabaseFixture databaseFixture)
        : base(output, databaseFixture)
    {
    }

    [Fact]
    [Trait("Category", "MongoDB")]
    public async Task EntityIdentifierQuery()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var identifierQuery = new EntityIdentifierQuery<string, PriorityReadModel>(MockPrincipal.Default, PriorityConstants.Normal.Id);
        var identifierResult = await mediator.Send(identifierQuery);
        identifierResult.Should().NotBeNull();
        identifierResult.Id.Should().Be(PriorityConstants.Normal.Id);
    }

    [Fact]
    [Trait("Category", "MongoDB")]
    public async Task EntityIdentifiersQuery()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var identifiers = new[]
        {
            PriorityConstants.Normal.Id,
            PriorityConstants.High.Id
        };

        var identifierQuery = new EntityIdentifiersQuery<string, PriorityReadModel>(MockPrincipal.Default, identifiers);
        var identifierResults = await mediator.Send(identifierQuery);

        identifierResults.Should().NotBeNull();
        identifierResults.Count.Should().Be(2);
    }

    [Fact]
    [Trait("Category", "MongoDB")]
    public async Task EntityQueryIn()
    {
        var mediator = ServiceProvider.GetService<IMediator>();
        mediator.Should().NotBeNull();

        var mapper = ServiceProvider.GetService<IMapper>();
        mapper.Should().NotBeNull();

        var identifiers = new[]
        {
            PriorityConstants.Normal.Id,
            PriorityConstants.High.Id
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
        listResult.Total.Should().Be(2);
    }

}
