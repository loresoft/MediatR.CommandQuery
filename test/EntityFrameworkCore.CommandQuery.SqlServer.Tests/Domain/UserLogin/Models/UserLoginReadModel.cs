using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models
{
    public partial class UserLoginReadModel
        : EntityReadModel
    {
        #region Generated Properties
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

        #endregion

    }
}
