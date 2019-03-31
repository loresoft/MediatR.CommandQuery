using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Queries
{
    public static partial class TenantExtensions
    {
        #region Generated Extensions
        public static EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant GetByKey(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant> queryable, Guid id)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant> GetByKeyAsync(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant> queryable, Guid id)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.Tenant> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        #endregion

    }
}
