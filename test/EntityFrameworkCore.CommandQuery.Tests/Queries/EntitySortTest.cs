using System;
using EntityFrameworkCore.CommandQuery.Queries;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EntityFrameworkCore.CommandQuery.Tests.Queries
{
    public class EntitySortTest : UnitTestBase
    {
        public EntitySortTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("Name:Ascending", "Name", "Ascending")]
        [InlineData("Name:Descending", "Name", "Descending")]
        [InlineData("Name : Descending", "Name", "Descending")]
        [InlineData("Name", "Name", null)]
        [InlineData("", null, null)]
        public void Parse(string source, string name, string direction)
        {
            var sort = EntitySort.Parse(source);

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(direction))
            {
                sort.Should().BeNull();
            }
            else
            {
                sort.Should().NotBeNull();
                sort.Name.Should().Be(name);
                sort.Direction.Should().Be(direction);
            }
        }
    }
}