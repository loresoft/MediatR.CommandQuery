using System;

using FluentAssertions;

using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Tests.Samples;

using Xunit;

namespace MediatR.CommandQuery.Tests.Queries;

public class EntityIdentifierQueryTests
{
    [Fact]
    public void ConstructorNull()
    {
        var identifierQuery = new EntityIdentifierQuery<Guid, LocationReadModel>(null, Guid.Empty);
        identifierQuery.Should().NotBeNull();
    }

    [Fact]
    public void ConstructorWithParameters()
    {
        var id = Guid.NewGuid();
        var identifierQuery = new EntityIdentifierQuery<Guid, LocationReadModel>(MockPrincipal.Default, id);
        identifierQuery.Should().NotBeNull();

        identifierQuery.Id.Should().NotBe(Guid.Empty);
        identifierQuery.Id.Should().Be(id);

        identifierQuery.Principal.Should().NotBeNull();
    }
}
