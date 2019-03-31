using System;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.Queries;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Constants;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Acceptance
{
    public class PriorityTests : DatabaseTestBase
    {
        public PriorityTests(ITestOutputHelper output, DatabaseFixture databaseFixture)
            : base(output, databaseFixture)
        {
        }

        [Fact]
        public async Task EntityIdentifierQuery()
        {
            var mediator = ServiceProvider.GetService<IMediator>();
            mediator.Should().NotBeNull();

            var mapper = ServiceProvider.GetService<IMapper>();
            mapper.Should().NotBeNull();

            var identifierQuery = new EntityIdentifierQuery<Guid, PriorityReadModel>(PriorityConstants.Normal, MockPrincipal.Default);
            var identifierResult = await mediator.Send(identifierQuery).ConfigureAwait(false);
            identifierResult.Should().NotBeNull();
            identifierResult.Id.Should().Be(PriorityConstants.Normal);
        }

        [Fact]
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

            var identifierQuery = new EntityIdentifiersQuery<Guid, PriorityReadModel>(identifiers, MockPrincipal.Default);
            var identifierResults = await mediator.Send(identifierQuery).ConfigureAwait(false);

            identifierResults.Should().NotBeNull();
            identifierResults.Count.Should().Be(2);
        }
    }
}
