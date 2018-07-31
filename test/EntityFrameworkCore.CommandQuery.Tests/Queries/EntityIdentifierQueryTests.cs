using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Queries;
using EntityFrameworkCore.CommandQuery.Tests.Samples;
using FluentAssertions;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.Tests.Queries
{
    public class EntityIdentifierQueryTests
    {
        [Fact]
        public void ConstructorNull()
        {
            var identifierQuery = new EntityIdentifierQuery<Guid, Location, LocationReadModel>(Guid.Empty, null);
            identifierQuery.Should().NotBeNull();
        }

        [Fact]
        public void ConstructorWithParameters()
        {
            var id = Guid.NewGuid();
            var identifierQuery = new EntityIdentifierQuery<Guid, Location, LocationReadModel>(id, MockPrincipal.Default);
            identifierQuery.Should().NotBeNull();

            identifierQuery.Id.Should().NotBe(Guid.Empty);
            identifierQuery.Id.Should().Be(id);

            identifierQuery.Principal.Should().NotBeNull();
        }
    }
}
