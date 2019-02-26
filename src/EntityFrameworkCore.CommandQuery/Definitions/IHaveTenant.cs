using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.CommandQuery.Definitions
{
    /// <summary>
    /// <c>Interface</c> indicating object supports multi-tenancy
    /// </summary>
    public interface IHaveTenant<TKey>
    {
        TKey TenantId { get; set; }
    }
}
