using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.Context
{
    public static class AppDbContextExtensions
    {
        public static void MarkCreatedItemAsOwnedBy(this DbContext context, long companyId)
        {
            foreach (var entityEntry in context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added))
            {
                if (entityEntry.Entity is _BaseEntity entityToMark)
                {
                    entityToMark.SetCompany(companyId);
                }
            }
        }
    }
    
}
