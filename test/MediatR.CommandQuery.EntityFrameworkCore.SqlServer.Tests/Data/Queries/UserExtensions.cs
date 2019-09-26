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
        public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User GetByEmailAddress(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefault(q => q.EmailAddress == emailAddress);
        }

        public static Task<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> GetByEmailAddressAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefaultAsync(q => q.EmailAddress == emailAddress);
        }

        public static MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User GetByKey(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> GetByKeyAsync(this IQueryable<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.User>(task);
        }

        #endregion

    }
}
