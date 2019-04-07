using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class StatusExtensions
    {
        #region Generated Extensions
        public static Status GetByKey(this IQueryable<Status> queryable, Guid id)
        {
            if (queryable is DbSet<Status> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<Status> GetByKeyAsync(this IQueryable<Status> queryable, Guid id)
        {
            if (queryable is DbSet<Status> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        #endregion

    }
}
