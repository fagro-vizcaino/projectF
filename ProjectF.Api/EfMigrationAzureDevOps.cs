using ProjectF.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjectF.Api
{
    /* this class is a trick to create a migration
     * without connection to ef core this will be use on 
     * Azure DevOps, Azure Pipeline will pick this to 
     * actually create the migration 
     * https://stackoverflow.com/questions/58390703/entity-framework-migration-azure-devops-release-pipeline
     */
    //public class EfMigrationAzureDevOps : IDesignTimeDbContextFactory<AbacusContext>
    //{
    //    public AbacusContext CreateDbContext(string[] args)
    //    {
    //        var databaseConnectionString = "dummy connection";
    //        var builder = new DbContextOptionsBuilder<AbacusContext>();
    //        builder.UseSqlServer(databaseConnectionString);

    //        return new AbacusContext(builder.Options);
    //    }
    //}
}
