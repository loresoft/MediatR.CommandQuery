using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class UserLoginExtensions
    {
        #region Generated Extensions
        public static IQueryable<UserLogin> ByEmailAddress(this IQueryable<UserLogin> queryable, string emailAddress)
        {
            return queryable.Where(q => q.EmailAddress == emailAddress);
        }

        public static UserLogin GetByKey(this IQueryable<UserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<UserLogin> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<UserLogin> GetByKeyAsync(this IQueryable<UserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<UserLogin> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        public static IQueryable<UserLogin> ByUserId(this IQueryable<UserLogin> queryable, Guid? userId)
        {
            return queryable.Where(q => (q.UserId == userId || (userId == null && q.UserId == null)));
        }

        #endregion

    }
}
