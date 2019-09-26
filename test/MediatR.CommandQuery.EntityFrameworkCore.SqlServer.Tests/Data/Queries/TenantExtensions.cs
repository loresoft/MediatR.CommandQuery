using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class TenantExtensions
    {
        #region Generated Extensions
        public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Tenant GetByKey(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Tenant> queryable, Guid id)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Tenant> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Tenant> GetByKeyAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Tenant> queryable, Guid id)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Tenant> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.Tenant>(task);
        }

        #endregion

    }
}
