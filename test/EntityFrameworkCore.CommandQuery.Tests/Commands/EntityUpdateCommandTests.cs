using System;
using DataGenerator;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Tests.Samples;
using FluentAssertions;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.Tests.Commands
{
    public class EntityUpdateCommandTests
    {
        [Fact]
        public void ConstructorNullModel()
        {
            Action act = () => new EntityUpdateCommand<Guid, LocationUpdateModel, LocationReadModel>(Guid.Empty, null, null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ConstructorWithModel()
        {
            var id = Guid.NewGuid();
            var updateModel = Generator.Default.Single<LocationUpdateModel>();
            updateModel.Should().NotBeNull();

            var updateCommand = new EntityUpdateCommand<Guid, LocationUpdateModel, LocationReadModel>(id, updateModel, MockPrincipal.Default);
            updateCommand.Should().NotBeNull();

            updateCommand.Id.Should().NotBe(Guid.Empty);
            updateCommand.Id.Should().Be(id);

            updateCommand.Model.Should().NotBeNull();

            updateCommand.Principal.Should().NotBeNull();
        }
    }
}
