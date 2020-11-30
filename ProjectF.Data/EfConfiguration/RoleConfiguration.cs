using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
              new IdentityRole
              {
                  Name = "Manager",
                  NormalizedName = "MANAGER"
              },
              new IdentityRole
              {
                  Name = "Admin",
                  NormalizedName = "Admin"
              },
                new IdentityRole
                {
                    Name = "Visitor",
                    NormalizedName = "Visitor"
                }
            );
        }
    }
}
