using System;

using FluentAssertions;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Tests.Samples;

using Microsoft.AspNetCore.JsonPatch;

using Xunit;

namespace MediatR.CommandQuery.Tests.Commands;

public class EntityPatchCommandTests
{
    [Fact]
    public void ConstructorNullModel()
    {
        Action act = () => new EntityPatchCommand<Guid, LocationReadModel>(null, Guid.Empty, null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ConstructorWithPatch()
    {
        var id = Guid.NewGuid();
        var jsonPatch = new JsonPatchDocument();
        jsonPatch.Replace("Name", "Test");

        var updateCommand = new EntityPatchCommand<Guid, LocationReadModel>(MockPrincipal.Default, id, jsonPatch);
        updateCommand.Should().NotBeNull();

        updateCommand.Id.Should().NotBe(Guid.Empty);
        updateCommand.Id.Should().Be(id);

        updateCommand.Patch.Should().NotBeNull();

        updateCommand.Principal.Should().NotBeNull();
    }
}
