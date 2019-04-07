using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class PriorityExtensions
    {
        #region Generated Extensions
        public static Priority GetByKey(this IQueryable<Priority> queryable, Guid id)
        {
            if (queryable is DbSet<Priority> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<Priority> GetByKeyAsync(this IQueryable<Priority> queryable, Guid id)
        {
            if (queryable is DbSet<Priority> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        #endregion

    }
}
