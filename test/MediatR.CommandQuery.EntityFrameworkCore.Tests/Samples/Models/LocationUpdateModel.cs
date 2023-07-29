using System;

using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.EntityFrameworkCore.Tests.Samples.Models;

public class LocationUpdateModel : EntityUpdateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public string PostalCode { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}
