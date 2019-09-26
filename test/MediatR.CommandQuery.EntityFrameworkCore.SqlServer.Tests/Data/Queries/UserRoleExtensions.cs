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
        public static IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> ByRoleId(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid roleId)
        {
            return queryable.Where(q => q.RoleId == roleId);
        }

        public static IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> ByUserId(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid userId)
        {
            return queryable.Where(q => q.UserId == userId);
        }

        public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole GetByKey(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> dbSet)
                return dbSet.Find(userId, roleId);

            return queryable.FirstOrDefault(q => q.UserId == userId
                && q.RoleId == roleId);
        }

        public static ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> GetByKeyAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> queryable, Guid userId, Guid roleId)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole> dbSet)
                return dbSet.FindAsync(userId, roleId);

            var task = queryable.FirstOrDefaultAsync(q => q.UserId == userId
                && q.RoleId == roleId);
            return new ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserRole>(task);
        }

        #endregion

    }
}
