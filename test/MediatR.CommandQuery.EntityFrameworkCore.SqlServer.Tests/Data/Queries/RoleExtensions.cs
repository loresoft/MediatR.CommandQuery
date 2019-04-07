using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class RoleExtensions
    {
        #region Generated Extensions
        public static Role GetByKey(this IQueryable<Role> queryable, Guid id)
        {
            if (queryable is DbSet<Role> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<Role> GetByKeyAsync(this IQueryable<Role> queryable, Guid id)
        {
            if (queryable is DbSet<Role> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        public static Role GetByName(this IQueryable<Role> queryable, string name)
        {
            return queryable.FirstOrDefault(q => q.Name == name);
        }

        public static Task<Role> GetByNameAsync(this IQueryable<Role> queryable, string name)
        {
            return queryable.FirstOrDefaultAsync(q => q.Name == name);
        }

        #endregion

    }
}
