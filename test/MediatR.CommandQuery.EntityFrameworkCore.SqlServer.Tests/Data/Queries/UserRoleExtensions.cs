using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Queries
{
    public static partial class UserRoleExtensions
    {
        #region Generated Extensions
        public static IQueryable<UserRole> ByRoleId(this IQueryable<UserRole> queryable, Guid roleId)
        {
            return queryable.Where(q => q.RoleId == roleId);
        }

        public static IQueryable<UserRole> ByUserId(this IQueryable<UserRole> queryable, Guid userId)
        {
            return queryable.Where(q => q.UserId == userId);
        }

        public static UserRole GetByKey(this IQueryable<UserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<UserRole> dbSet)
                return dbSet.Find(userId, roleId);

            return queryable.FirstOrDefault(q => q.UserId == userId
                && q.RoleId == roleId);
        }

        public static Task<UserRole> GetByKeyAsync(this IQueryable<UserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<UserRole> dbSet)
                return dbSet.FindAsync(userId, roleId);

            return queryable.FirstOrDefaultAsync(q => q.UserId == userId
                && q.RoleId == roleId);
        }

        #endregion

    }
}
