using Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Notes : pourrait se généraliser via génériques
/// </summary>
namespace Contexts
{
    public class WikiBeerSqlContextFactory : IDesignTimeDbContextFactory<WikiBeerSqlContext>
    {
        public WikiBeerSqlContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WikiBeerSqlContext>();
            optionsBuilder.UseSqlServer(DefaultMigrationString.DEFAULT_MIGRATION_STRING);

            return new WikiBeerSqlContext(optionsBuilder.Options);
        }
    }
}
