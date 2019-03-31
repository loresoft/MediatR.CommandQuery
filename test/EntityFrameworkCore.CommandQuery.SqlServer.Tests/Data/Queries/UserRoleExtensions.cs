using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Queries
{
    public static partial class UserRoleExtensions
    {
        #region Generated Extensions
        public static IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole> ByRoleId(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid roleId)
        {
            return queryable.Where(q => q.RoleId == roleId);
        }

        public static IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole> ByUserId(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid userId)
        {
            return queryable.Where(q => q.UserId == userId);
        }

        public static EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole GetByKey(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole> dbSet)
                return dbSet.Find(userId, roleId);

            return queryable.FirstOrDefault(q => q.UserId == userId
                && q.RoleId == roleId);
        }

        public static Task<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole> GetByKeyAsync(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.UserRole> dbSet)
                return dbSet.FindAsync(userId, roleId);

            return queryable.FirstOrDefaultAsync(q => q.UserId == userId
                && q.RoleId == roleId);
        }

        #endregion

    }
}
