using System;

using DataGenerator;

using FluentAssertions;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Tests.Samples;

using Xunit;

namespace MediatR.CommandQuery.Tests.Commands;

public class EntityUpdateCommandTests
{
    [Fact]
    public void ConstructorNullModel()
    {
        Action act = () => new EntityUpdateCommand<Guid, LocationUpdateModel, LocationReadModel>(null, Guid.Empty, null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ConstructorWithModel()
    {
        var id = Guid.NewGuid();
        var updateModel = Generator.Default.Single<LocationUpdateModel>();
        updateModel.Should().NotBeNull();

        var updateCommand = new EntityUpdateCommand<Guid, LocationUpdateModel, LocationReadModel>(MockPrincipal.Default, id, updateModel);
        updateCommand.Should().NotBeNull();

        updateCommand.Id.Should().NotBe(Guid.Empty);
        updateCommand.Id.Should().Be(id);

        updateCommand.Model.Should().NotBeNull();

        updateCommand.Principal.Should().NotBeNull();
    }
}
