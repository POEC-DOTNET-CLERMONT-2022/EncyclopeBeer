using Ipme.WikiBeer.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Tools
{
    public class DbManager
    {
        private string ConnectionString { get; }

        public DbManager(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Connection string is mandatory");
            ConnectionString = connectionString;
        }

        public void EnsureDatabaseCreation()
        {
            using (var context = new WikiBeerSqlContext(GetContextOptions()))
            {
                context.Database.EnsureCreated();
            }
        }

        public void DropDataBase()
        {
            using (var context = new WikiBeerSqlContext(GetContextOptions()))
            {
                context.Database.EnsureDeleted();
            }
        }

        private DbContextOptions<WikiBeerSqlContext> GetContextOptions()
        {
            var contextOptionBuilder = new DbContextOptionsBuilder<WikiBeerSqlContext>();
            contextOptionBuilder.UseSqlServer(ConnectionString);
            return contextOptionBuilder.Options;
        }
    }
}
