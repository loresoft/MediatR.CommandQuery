using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Queries
{
    public static partial class UserExtensions
    {
        #region Generated Extensions
        public static EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User GetByEmailAddress(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefault(q => q.EmailAddress == emailAddress);
        }

        public static Task<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User> GetByEmailAddressAsync(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefaultAsync(q => q.EmailAddress == emailAddress);
        }

        public static EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User GetByKey(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static Task<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User> GetByKeyAsync(this IQueryable<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<EntityFrameworkCore.CommandQuery.SqlServer.Tests.Data.Entities.User> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        #endregion

    }
}
