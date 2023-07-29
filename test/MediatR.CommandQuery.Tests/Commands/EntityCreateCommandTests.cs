using System;

using DataGenerator;

using FluentAssertions;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Tests.Samples;

using Xunit;

namespace MediatR.CommandQuery.Tests.Commands;

public class EntityCreateCommandTests
{
    [Fact]
    public void ConstructorNullModel()
    {
        Action act = () => new EntityCreateCommand<LocationCreateModel, LocationReadModel>(null, null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ConstructorWithModel()
    {
        var createModel = Generator.Default.Single<LocationCreateModel>();
        createModel.Should().NotBeNull();

        var createCommand = new EntityCreateCommand<LocationCreateModel, LocationReadModel>(MockPrincipal.Default, createModel);
        createCommand.Should().NotBeNull();
        createCommand.Model.Should().NotBeNull();
        createCommand.Principal.Should().NotBeNull();
    }
}
