using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

using FluentAssertions;

using MediatR.CommandQuery.Queries;

using Xunit;

namespace MediatR.CommandQuery.Tests.Queries
{
    public class EntityFilterJsonTests
    {
        [Fact]
        public void ParseStringJson()
        {
            var json = "{\"name\":\"Name\",\"operator\":\"eq\",\"value\":\"test\"}";

            var filter = JsonSerializer.Deserialize<EntityFilter>(json);

            filter.Name.Should().Be("Name");
            filter.Operator.Should().Be("eq");

            filter.Value.Should().BeOfType<string>();
            filter.Value.Should().Be("test");
        }

        [Fact]
        public void ParseBooleanJson()
        {
            var json = "{\"name\":\"Name\",\"operator\":\"eq\",\"value\":true}";

            var filter = JsonSerializer.Deserialize<EntityFilter>(json);

            filter.Value.Should().BeOfType<bool>();
            filter.Value.Should().Be(true);
        }

        [Fact]
        public void ParseNumberJson()
        {
            var json = "{\"name\":\"Name\",\"operator\":\"eq\",\"value\":123}";

            var filter = JsonSerializer.Deserialize<EntityFilter>(json);

            filter.Value.Should().BeOfType<double>();
            filter.Value.Should().Be(123);
        }


        [Fact]
        public void ParseQueryJson()
        {
            var json = "{\"page\":1,\"pageSize\":20,\"sort\":[{\"name\":\"Name\",\"direction\":\"asc\"}],\"filter\":{\"logic\":\"or\",\"filters\":[{\"name\":\"Name\",\"operator\":\"eq\",\"value\":\"test\"},{\"name\":\"Description\",\"operator\":\"eq\",\"value\":\"test\"}]}}";
            var query = JsonSerializer.Deserialize<EntityQuery>(json);

            query.Page.Should().Be(1);
            query.PageSize.Should().Be(20);
            query.Query.Should().BeNull();

            query.Sort.Should().NotBeEmpty();
            query.Sort.Should().HaveCount(1);
            query.Sort[0].Name.Should().Be("Name");
            query.Sort[0].Direction.Should().Be("asc");

            query.Filter.Should().NotBeNull();
            query.Filter.Logic.Should().Be("or");
            query.Filter.Filters.Should().HaveCount(2);

            query.Filter.Filters[0].Name.Should().Be("Name");
            query.Filter.Filters[0].Operator.Should().Be("eq");

            query.Filter.Filters[0].Value.Should().BeOfType<string>();
            query.Filter.Filters[0].Value.Should().Be("test");

            query.Filter.Filters[1].Name.Should().Be("Description");
            query.Filter.Filters[1].Operator.Should().Be("eq");

            query.Filter.Filters[1].Value.Should().BeOfType<string>();
            query.Filter.Filters[1].Value.Should().Be("test");

        }

        [Fact]
        public void SerializeRoundTripFilter()
        {
            var filter = new EntityFilter
            {
                Logic = EntityFilterLogic.And,
                Filters = new List<EntityFilter>
                {
                    new EntityFilter { Name = "IsDeleted", Value = true, Operator = EntityFilterOperators.Equal },
                    new EntityFilter { Name = "StatusId", Value = "1234", Operator = EntityFilterOperators.Equal }
                }
            };

            var json = JsonSerializer.Serialize(filter);
            json.Should().NotBeNullOrWhiteSpace();
            json.Should().Be("{\"logic\":\"and\",\"filters\":[{\"name\":\"IsDeleted\",\"operator\":\"eq\",\"value\":true},{\"name\":\"StatusId\",\"operator\":\"eq\",\"value\":\"1234\"}]}");

            var filterDeserialize = JsonSerializer.Deserialize<EntityFilter>(json);
            filterDeserialize.Should().NotBeNull();

            filterDeserialize.Logic.Should().Be("and");
            filterDeserialize.Filters.Should().HaveCount(2);

            filterDeserialize.Filters[0].Name.Should().Be("IsDeleted");
            filterDeserialize.Filters[0].Operator.Should().Be("eq");

            filterDeserialize.Filters[0].Value.Should().BeOfType<bool>();
            filterDeserialize.Filters[0].Value.Should().Be(true);

            filterDeserialize.Filters[1].Name.Should().Be("StatusId");
            filterDeserialize.Filters[1].Operator.Should().Be("eq");

            filterDeserialize.Filters[1].Value.Should().BeOfType<string>();
            filterDeserialize.Filters[1].Value.Should().Be("1234");

        }

    }
}
