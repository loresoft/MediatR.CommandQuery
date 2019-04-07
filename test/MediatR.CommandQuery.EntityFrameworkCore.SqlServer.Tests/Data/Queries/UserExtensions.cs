using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class UserExtensions
    {
        #region Generated Extensions
        public static User GetByEmailAddress(this IQueryable<User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefault(q => q.EmailAddress == emailAddress);
        }

        public static Task<User> GetByEmailAddressAsync(this IQueryable<User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefaultAsync(q => q.EmailAddress == emailAddress);
        }

        public static User GetByKey(this IQueryable<User> queryable, Guid id)
        {
            if (queryable is DbSet<User> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<User> GetByKeyAsync(this IQueryable<User> queryable, Guid id)
        {
            if (queryable is DbSet<User> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        #endregion

    }
}
