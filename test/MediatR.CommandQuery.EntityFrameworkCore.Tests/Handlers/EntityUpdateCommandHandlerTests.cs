using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataGenerator;
using FluentAssertions;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.EntityFrameworkCore.Handlers;
using MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples;
using MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples.Data;
using MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace MediatR.CommandQuery.EntityFrameworkCore.Tests.Handlers
{
    public class EntityUpdateCommandHandlerTests
    {
        [Fact]
        public async Task UpdateLocation()
        {
            var original = Generator.Default.Single<Location>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LocationCreateModel, Location>();
                cfg.CreateMap<LocationUpdateModel, Location>();
                cfg.CreateMap<Location, LocationReadModel>();
            });
            var mapper = new Mapper(config);

            var options = new DbContextOptionsBuilder<SampleContext>()
                .UseInMemoryDatabase(databaseName: "UpdateLocation")
                .Options;

            var context = new SampleContext(options);
            context.Locations.Add(original);
            context.SaveChanges();

            var updateModel = Generator.Default.Single<LocationUpdateModel>();
            updateModel.Should().NotBeNull();

            var updateCommand = new EntityUpdateCommand<Guid, LocationUpdateModel, LocationReadModel>(MockPrincipal.Default, original.Id, updateModel);
            updateCommand.Should().NotBeNull();
            updateCommand.Model.Should().NotBeNull();
            updateCommand.Principal.Should().NotBeNull();

            var updateCommandHandler = new EntityUpdateCommandHandler<SampleContext, Location, Guid, LocationUpdateModel, LocationReadModel>(NullLoggerFactory.Instance, context, mapper);
            var readModel = await updateCommandHandler.Handle(updateCommand, CancellationToken.None);

            readModel.Should().NotBeNull();
            readModel.Id.Should().NotBe(Guid.Empty);
            readModel.Id.Should().Be(original.Id);

            readModel.Name.Should().Be(updateModel.Name);
        }

    }
}