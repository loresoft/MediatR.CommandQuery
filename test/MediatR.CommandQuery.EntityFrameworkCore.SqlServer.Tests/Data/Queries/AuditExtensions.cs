using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class AuditExtensions
    {
        #region Generated Extensions
        public static Audit GetByKey(this IQueryable<Audit> queryable, Guid id)
        {
            if (queryable is DbSet<Audit> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<Audit> GetByKeyAsync(this IQueryable<Audit> queryable, Guid id)
        {
            if (queryable is DbSet<Audit> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        #endregion

    }
}
