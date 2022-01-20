using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contexts
{
    public class WikiBeerSqlContext : DbContext
    {
        // Tables : absolument nécessaire pour utiliser l'API (pas vraiment certain sa, à tester une fois la bdd remplie)
        public DbSet<BeerEntity> Beers { get; set; }
        public DbSet<BreweryEntity> Brewerys { get; set; }
        public DbSet<BeerColorEntity> BeerColors { get; set; }
        public DbSet<BeerStyleEntity> BeerStyles { get; set; }
        public DbSet<CountryEntity> Countrys { get; set; }
        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<HopEntity> Hops { get; set; }
        public DbSet<AdditiveEntity> Additives { get; set; }
        public DbSet<CerealEntity> Cereals { get; set; }

        // Gestion
        public string ConnectionString { get; private set; }

        //public WikiBeerSqlContext()
        //{
        //}

        /// <summary>
        /// Nécessaire au bon fonctionnement avec l'API (AddDbContext) et de la factory de migration
        /// </summary>
        /// <param name="options"></param>
        public WikiBeerSqlContext(DbContextOptions<WikiBeerSqlContext> options) : base(options)
        {
        }

        /// <summary>
        /// Pour test divers et BddToAPIManager(pour donner une connection string)
        /// </summary>
        /// <param name = "connectionString" ></ param >
        public WikiBeerSqlContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Pour test divers et BddToAPIManager (pour donner une connection string)
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // comportement par défaut si l'on ne fournit pas de connection string au constructeur (via AddDbContext dans l'api -> on passe par DbContextOptions<>...)
            base.OnConfiguring(optionsBuilder);
            if (string.IsNullOrEmpty(ConnectionString)) 
                return;
            else 
                optionsBuilder.UseSqlServer(ConnectionString); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Beers : a désactiver pour les test fixture -> a réactiver pour la migration Property(t => t.ThemeId)
            //EntityTypeBuilder<BeerEntity> beerTypeBuilder = modelBuilder.Entity<BeerEntity>(mb => mb.Property(b => b.Id).ValueGeneratedOnAdd();); // Version pour générer les clefs à la volée via la base
            EntityTypeBuilder<BeerEntity> beerTypeBuilder = modelBuilder.Entity<BeerEntity>();
            beerTypeBuilder.HasMany(b => b.Ingredients)
                           .WithMany(i => i.Beers)
                           .UsingEntity(bi => bi.ToTable("BeerIngredient")); // permet de faire la table entity de manière automatique
            beerTypeBuilder.Navigation(b => b.Ingredients).AutoInclude(); //Chargement automatique de la propriété de dépendance

            //Si pas d'auto-include alors on doit charger en 2 fois
            //BeerEntity beer = GetById();
            //beer.Ingredients = GetIngredients(beer.BeerId);

            //Ingredients
            //EntityTypeBuilder<IngredientEntity> ingredientTypeBuilder = modelBuilder.Entity<IngredientEntity>();
            //ingredientTypeBuilder.HasMany(i => i.Beers)
            //                     .WithMany(b => b.Ingredients)
            //                     .UsingEntity(bi => bi.ToTable("BeersIngredients"));
            //ingredientTypeBuilder.Navigation(i => i.Beers).AutoInclude();

            EntityTypeBuilder<IngredientEntity> ingredientBuilder = modelBuilder.Entity<IngredientEntity>();


            // Configuration du Discriminateur de sous type dans la table BeerIngredient
            ingredientBuilder.HasDiscriminator<string>("Type")
                .HasValue<HopEntity>("Hop")
                .HasValue<CerealEntity>("Cereal")
                .HasValue<AdditiveEntity>("Additive");
            ingredientBuilder.Property("Type").HasMaxLength(50);
            //EntityTypeBuilder<HopEntity> hopTypeBuilder = modelBuilder.Entity<HopEntity>();
            //hopTypeBuilder.HasDiscriminator<string>(typeof(HopEntity).Name); // ne fonctionen pas comme voulue, ajoute simplement une colonne
            // au lieu de remplacer donner une valeur à la colonne Discriminator

            // BeersIngredients --> plus besoin car on charge directement la liste d'ingrédients dans la bière (et inversement) sans passer par la table d'association
            //EntityTypeBuilder<BeerIngredientEntity> beerIngredientTypeBuilder = modelBuilder.Entity<BeerIngredientEntity>();
            //beerIngredientTypeBuilder.HasKey(bi => new { bi.BeerId, bi.IngredientId });
            //beerIngredientTypeBuilder.HasOne(bi => bi.IngredientId).WithOne();
            //beerIngredientTypeBuilder.Navigation(beer => beer.Ingredients).AutoInclude();
        }


        public override DbSet<TEntity> Set<TEntity>()
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return base.Set<TEntity>();
        }

    }

}
