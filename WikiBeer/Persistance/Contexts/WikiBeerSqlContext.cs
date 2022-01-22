using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Entities.Ingredients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance.Contexts
{
    public class WikiBeerSqlContext : DbContext
    {
        #region Tables (DbSet)
        // Tables : absolument nécessaire pour utiliser l'API (et faire la migration)
        public DbSet<BeerEntity> Beers { get; set; }
        public DbSet<BreweryEntity> Brewerys { get; set; }
        public DbSet<BeerColorEntity> BeerColors { get; set; }
        public DbSet<BeerStyleEntity> BeerStyles { get; set; }
        public DbSet<CountryEntity> Countrys { get; set; }
        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<HopEntity> Hops { get; set; }
        public DbSet<AdditiveEntity> Additives { get; set; }
        public DbSet<CerealEntity> Cereals { get; set; }
        #endregion
        
        /// <summary>
        /// Nécessaire au bon fonctionnement avec l'API (AddDbContext) et de la factory de migration
        /// </summary>
        /// <param name="options"></param>
        public WikiBeerSqlContext(DbContextOptions<WikiBeerSqlContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnBeerCreating(modelBuilder);
            OnBreweryCreating(modelBuilder);
            OnStyleCreating(modelBuilder);
            OnColorCreating(modelBuilder);
            OnIngredientCreating(modelBuilder);
        }

        #region Méthodes de configuration des models
        private void OnBeerCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<BeerEntity> typeBuilder = modelBuilder.Entity<BeerEntity>();
            
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("Beer").HasKey(be => be.Id).HasName("BeerId");
            typeBuilder.Property(be => be.Id).ValueGeneratedOnAdd();

            #region Configuration relations
            // Brewery
            typeBuilder.HasOne(be => be.Brewery).WithMany(br => br.Beers);
            //.HasForeignKey(be => be.Brewery);
            // Style
            typeBuilder.HasOne(be => be.Style).WithMany();
            // Color
            typeBuilder.HasOne(be => be.Color).WithMany();
            // Ingredients - BeerIngredient
            typeBuilder.HasMany(b => b.Ingredients).WithMany(i => i.Beers)
                           .UsingEntity(bi => bi.ToTable("BeerIngredient")); // permet de faire la table entity de manière automatique
            //beerTypeBuilder.Navigation(b => b.Ingredients).AutoInclude(); //Chargement automatique de la propriété de dépendance
            #endregion
        }

        private void OnBreweryCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<BreweryEntity> typeBuilder = modelBuilder.Entity<BreweryEntity>();
            
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("Brewery").HasKey(br => br.Id).HasName("BreweryId");
            typeBuilder.Property(br => br.Id).ValueGeneratedOnAdd();

            #region Configuration relations
            // Beers
            typeBuilder.HasMany(br => br.Beers).WithOne(be => be.Brewery);
            #endregion
        }

        private void OnStyleCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<BeerStyleEntity> typeBuilder = modelBuilder.Entity<BeerStyleEntity>();
            
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("BeerStyle").HasKey(s => s.Id).HasName("StyleId");
            typeBuilder.Property(s => s.Id).ValueGeneratedOnAdd();

            #region Configuration relations
            #endregion
        }

        private void OnColorCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<BreweryEntity> typeBuilder = modelBuilder.Entity<BreweryEntity>();
            
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("BeerColor").HasKey(c => c.Id).HasName("ColorId");
            typeBuilder.Property(c => c.Id).ValueGeneratedOnAdd();

            #region Configuration relations
            #endregion
        }

        private void OnIngredientCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<IngredientEntity> typeBuilder = modelBuilder.Entity<IngredientEntity>();
            
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("Ingredient").HasKey(i => i.Id).HasName("IngredientId");
            typeBuilder.Property(i => i.Id).ValueGeneratedOnAdd();

            #region Configuration relations
            // BeerIngredient
            typeBuilder.HasDiscriminator<string>("Type")
                .HasValue<HopEntity>("Hop")
                .HasValue<CerealEntity>("Cereal")
                .HasValue<AdditiveEntity>("Additive");
            typeBuilder.Property("Type").HasMaxLength(50);
            #endregion
        }

        #endregion

        public override DbSet<TEntity> Set<TEntity>()
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return base.Set<TEntity>();
        }

    }
}
