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
        public static Tenant GetByKey(this IQueryable<Tenant> queryable, Guid id)
        {
            if (queryable is DbSet<Tenant> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<Tenant> GetByKeyAsync(this IQueryable<Tenant> queryable, Guid id)
        {
            if (queryable is DbSet<Tenant> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        #endregion

    }
}
