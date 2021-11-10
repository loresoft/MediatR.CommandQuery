using System.Threading.Tasks;

using AutoMapper;

using FluentAssertions;

using MediatR.CommandQuery.Cosmos.Tests.Constants;
using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.DependencyInjection;

using Xunit;
using Xunit.Abstractions;

namespace MediatR.CommandQuery.Cosmos.Tests.Acceptance
{
    public class PriorityTests : DatabaseTestBase
    {
        public PriorityTests(ITestOutputHelper output, DatabaseFixture databaseFixture)
            : base(output, databaseFixture)
        {
        }

        [Fact]
        [Trait("Category", "Cosmos")]
        public async Task EntityIdentifierQuery()
        {
            var mediator = ServiceProvider.GetService<IMediator>();
            mediator.Should().NotBeNull();

            var mapper = ServiceProvider.GetService<IMapper>();
            mapper.Should().NotBeNull();

            var identifierQuery = new EntityIdentifierQuery<string, PriorityReadModel>(MockPrincipal.Default, PriorityConstants.Normal.ToCosmosKey());
            var identifierResult = await mediator.Send(identifierQuery).ConfigureAwait(false);
            identifierResult.Should().NotBeNull();
            identifierResult.Id.Should().Be(PriorityConstants.Normal.Id);
        }

        [Fact]
        [Trait("Category", "Cosmos")]
        public async Task EntityIdentifiersQuery()
        {
            var mediator = ServiceProvider.GetService<IMediator>();
            mediator.Should().NotBeNull();

            var mapper = ServiceProvider.GetService<IMapper>();
            mapper.Should().NotBeNull();

            var identifiers = new[]
            {
                PriorityConstants.Normal.ToCosmosKey(),
                PriorityConstants.High.ToCosmosKey()
            };

            var identifierQuery = new EntityIdentifiersQuery<string, PriorityReadModel>(MockPrincipal.Default, identifiers);
            var identifierResults = await mediator.Send(identifierQuery).ConfigureAwait(false);

            identifierResults.Should().NotBeNull();
            identifierResults.Count.Should().Be(2);
        }
    }
}
