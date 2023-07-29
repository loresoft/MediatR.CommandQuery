using System;
using System.Collections.Generic;

namespace Tracker.WebService.Domain.Models;

public partial class RoleReadModel
    : EntityReadModel
{
    #region Generated Properties
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    #endregion

}
