using System;

using MediatR.CommandQuery.Definitions;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Entities
{
    public class UserLogin : MongoEntity, IHaveIdentifier<string>
    {
        public string EmailAddress { get; set; }

        public Guid? UserId { get; set; }

        public string UserAgent { get; set; }

        public string Browser { get; set; }

        public string OperatingSystem { get; set; }

        public string DeviceFamily { get; set; }

        public string DeviceBrand { get; set; }

        public string DeviceModel { get; set; }

        public string IpAddress { get; set; }

        public bool IsSuccessful { get; set; }

        public string FailureMessage { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
