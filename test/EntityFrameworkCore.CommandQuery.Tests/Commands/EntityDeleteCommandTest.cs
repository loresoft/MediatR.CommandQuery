using System;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Tests.Samples;
using FluentAssertions;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.Tests.Commands
{
    public class EntityDeleteCommandTest
    {
        [Fact]
        public void ConstructorWithId()
        {
            var id = Guid.NewGuid();
            var deleteCommand = new EntityDeleteCommand<Guid, LocationReadModel>(id, MockPrincipal.Default);

            deleteCommand.Should().NotBeNull();
            deleteCommand.Id.Should().NotBe(Guid.Empty);
            deleteCommand.Id.Should().Be(id);

            deleteCommand.Principal.Should().NotBeNull();

        }
    }
}