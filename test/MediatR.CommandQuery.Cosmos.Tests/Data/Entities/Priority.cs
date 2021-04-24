using Cosmos.Abstracts;
using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Cosmos.Tests.Data.Entities
{
    public class Priority : CosmosEntity, IHaveIdentifier<string>
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
