using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Constants;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority.Models;
using MediatR.CommandQuery.Queries;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Acceptance
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

            var identifierQuery = new EntityIdentifierQuery<Guid, PriorityReadModel>(MockPrincipal.Default, PriorityConstants.Normal);
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

            var identifierQuery = new EntityIdentifiersQuery<Guid, PriorityReadModel>(MockPrincipal.Default, identifiers);
            var identifierResults = await mediator.Send(identifierQuery).ConfigureAwait(false);

            identifierResults.Should().NotBeNull();
            identifierResults.Count.Should().Be(2);
        }
    }
}
