using System;
using DataGenerator;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Tests.Samples;
using FluentAssertions;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.Tests.Commands
{
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

            var createCommand = new EntityCreateCommand<LocationCreateModel, LocationReadModel>(createModel, MockPrincipal.Default);
            createCommand.Should().NotBeNull();
            createCommand.Model.Should().NotBeNull();
            createCommand.Principal.Should().NotBeNull();

            createCommand.Model.Created.Should().NotBe(DateTimeOffset.MinValue);
            createCommand.Model.CreatedBy.Should().Be("test@mailinator.com");

            createCommand.Model.Updated.Should().NotBe(DateTimeOffset.MinValue);
            createCommand.Model.UpdatedBy.Should().Be("test@mailinator.com");
        }
    }
}
