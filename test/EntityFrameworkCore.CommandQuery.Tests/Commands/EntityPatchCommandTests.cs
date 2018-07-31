using System;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Tests.Samples;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Xunit;

namespace EntityFrameworkCore.CommandQuery.Tests.Commands
{
    public class EntityPatchCommandTests
    {
        [Fact]
        public void ConstructorNullModel()
        {
            Action act = () => new EntityPatchCommand<Guid, Location, LocationReadModel>(Guid.Empty, null, null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ConstructorWithPatch()
        {
            var id = Guid.NewGuid();
            var jsonPatch = new JsonPatchDocument<Location>();
            jsonPatch.Replace(p => p.Name, "Test");

            var updateCommand = new EntityPatchCommand<Guid, Location, LocationReadModel>(id, jsonPatch, MockPrincipal.Default);
            updateCommand.Should().NotBeNull();

            updateCommand.Id.Should().NotBe(Guid.Empty);
            updateCommand.Id.Should().Be(id);

            updateCommand.Patch.Should().NotBeNull();

            updateCommand.Principal.Should().NotBeNull();
            updateCommand.Original.Should().BeNull();
        }
    }
}
