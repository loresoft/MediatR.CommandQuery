using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using FluentAssertions;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Constants;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority.Models;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.DependencyInjection;

using Xunit;
using Xunit.Abstractions;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Acceptance;

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
        var identifierResult = await mediator.Send(identifierQuery).ConfigureAwait(false);
        identifierResult.Should().NotBeNull();
        identifierResult.Id.Should().Be(PriorityConstants.Normal);
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
        var identifierResults = await mediator.Send(identifierQuery).ConfigureAwait(false);

        identifierResults.Should().NotBeNull();
        identifierResults.Count.Should().Be(2);
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

        var listResult = await mediator.Send(listQuery).ConfigureAwait(false);
        listResult.Should().NotBeNull();
        listResult.Total.Should().Be(2);
    }
}
