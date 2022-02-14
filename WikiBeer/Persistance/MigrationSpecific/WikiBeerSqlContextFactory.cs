using Ipme.WikiBeer.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design; // Pour IDesignTimeDbContextFactory
using Microsoft.Extensions.Configuration; // pour set path et IConfigurationRoot
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance.MigrationSpecific
{
    public class WikiBeerSqlContextFactory : IDesignTimeDbContextFactory<WikiBeerSqlContext>
    {
        public WikiBeerSqlContext CreateDbContext(string[] args)
        {
            //var cc = args.ToList();
            //cc.Add("bb");
            //Console.WriteLine(cc[0]);
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(@Directory.GetCurrentDirectory() + "/../API/appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var builder = new DbContextOptionsBuilder<WikiBeerSqlContext>();
            builder.UseSqlServer(connectionString);
            return new WikiBeerSqlContext(builder.Options);
        }
    }
}
