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
        public static IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin> ByEmailAddress(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin> queryable, string emailAddress)
        {
            return queryable.Where(q => q.EmailAddress == emailAddress);
        }

        public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin GetByKey(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin> GetByKeyAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin>(task);
        }

        public static IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin> ByUserId(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.UserLogin> queryable, Guid? userId)
        {
            return queryable.Where(q => (q.UserId == userId || (userId == null && q.UserId == null)));
        }

        #endregion

    }
}
