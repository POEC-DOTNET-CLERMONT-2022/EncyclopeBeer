using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Entities.Ingredients;
using Ipme.WikiBeer.Persistance.Contexts.Magics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// TODO : s'intéresser au splitting de requete pour optimiser les performances : 
/// voir : https://docs.microsoft.com/fr-fr/ef/core/querying/single-split-queries
/// Sur la différence entre Attach et Add : (important pour de l'insertion d'une enity qui conteint des sous objets déjà en base!)
/// voir : https://stackoverflow.com/questions/65401099/entity-framework-5-adding-existing-entity-to-nested-collection
/// Pour les différents Etats d'une entité (relié au point précédent)
/// coir : https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entitystate?view=efcore-5.0#microsoft-entityframeworkcore-entitystate-added
/// Pour faire de la validation (sur les valeur numériques par exemple)
/// voir : https://github.com/FluentValidation/FluentValidation
/// </summary>
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
        // TODO : vérifier si ces DbSet la sont vraiment nécessaire...
        public DbSet<HopEntity> Hops { get; set; }
        public DbSet<CerealEntity> Cereal { get; set; }
        public DbSet<AdditiveEntity> Additive { get; set; }

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

            // Entity de base
            OnBeerCreating(modelBuilder);
            OnBreweryCreating(modelBuilder);
            OnStyleCreating(modelBuilder);
            OnColorCreating(modelBuilder);
            OnCountryCreating(modelBuilder);

            // Enity Abstract et dérivée            
            OnIngredientCreating(modelBuilder);
            OnHopCreating(modelBuilder);
            OnCerealCreating(modelBuilder);
            OnAdditiveCreating(modelBuilder);

        }

        #region Méthodes de configuration des models
        private void OnBeerCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<BeerEntity> typeBuilder = modelBuilder.Entity<BeerEntity>();
            var idName = "BeerId";
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("Beer").HasKey(be => be.Id).HasName(idName);
            typeBuilder.Property(be => be.Id).HasColumnName(idName).ValueGeneratedOnAdd();
            // Configuration longueur des nvarchar 
            typeBuilder.Property(be => be.Name).HasMaxLength(Rules.DEFAULT_NAME_MAX_LENGHT);
            typeBuilder.Property(be => be.Description).HasMaxLength(Rules.DEFAULT_DESCRIPTION_MAX_LENGTH);

            #region Configuration relations
            // Brewery
            typeBuilder.HasOne(be => be.Brewery).WithMany();
            typeBuilder.Navigation(be => be.Brewery).AutoInclude();
            // Style
            typeBuilder.HasOne(be => be.Style).WithMany();
            typeBuilder.Navigation(be => be.Style).AutoInclude();
            // Color
            typeBuilder.HasOne(be => be.Color).WithMany();
            typeBuilder.Navigation(be => be.Color).AutoInclude();
            // Ingredients - BeerIngredient
            typeBuilder.HasMany(b => b.Ingredients).WithMany(i => i.Beers)
                           .UsingEntity(bi => bi.ToTable("BeerIngredient")); // permet de faire la table entity de manière automatique
            typeBuilder.Navigation(b => b.Ingredients).AutoInclude(); //Chargement automatique de la propriété de dépendance
            //Si pas d'auto-include alors on doit charger en 2 fois
            //BeerEntity beer = GetById();
            //beer.Ingredients = GetIngredients(beer.BeerId);
            // -> On tue alors l'interet du repos générique!!!
            #endregion
        }

        private void OnBreweryCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<BreweryEntity> typeBuilder = modelBuilder.Entity<BreweryEntity>();
            var idName = "BreweryId";
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("Brewery").HasKey(br => br.Id).HasName(idName);
            typeBuilder.Property(br => br.Id).HasColumnName(idName).ValueGeneratedOnAdd();
            // Configuration longueur des nvarchar 
            typeBuilder.Property(br => br.Name).HasMaxLength(Rules.DEFAULT_NAME_MAX_LENGHT);
            typeBuilder.Property(br => br.Description).HasMaxLength(Rules.DEFAULT_DESCRIPTION_MAX_LENGTH);

            #region Configuration relations
            // Country 
            typeBuilder.HasOne(br => br.Country).WithMany();// WithMany(c => c.Breweries);
            typeBuilder.Navigation(br => br.Country).AutoInclude();
            #endregion
        }

        private void OnStyleCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<BeerStyleEntity> typeBuilder = modelBuilder.Entity<BeerStyleEntity>();
            var idName = "StyleId";
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("BeerStyle").HasKey(s => s.Id).HasName(idName);
            typeBuilder.Property(s => s.Id).HasColumnName(idName).ValueGeneratedOnAdd();
            // Configuration longueur des nvarchar 
            typeBuilder.Property(s => s.Name).HasMaxLength(Rules.DEFAULT_NAME_MAX_LENGHT);
            typeBuilder.Property(s => s.Description).HasMaxLength(Rules.DEFAULT_DESCRIPTION_MAX_LENGTH);

            #region Configuration relations
            #endregion
        }

        private void OnColorCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<BeerColorEntity> typeBuilder = modelBuilder.Entity<BeerColorEntity>();
            var idName = "ColorId";
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("BeerColor").HasKey(c => c.Id).HasName(idName);
            typeBuilder.Property(c => c.Id).HasColumnName(idName).ValueGeneratedOnAdd();
            // Configuration longueur des nvarchar 
            typeBuilder.Property(c => c.Name).HasMaxLength(Rules.DEFAULT_NAME_MAX_LENGHT);
            
            #region Configuration relations
            #endregion
        }

        private void OnCountryCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<CountryEntity> typeBuilder = modelBuilder.Entity<CountryEntity>();
            var idName = "CountryId";
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("Country").HasKey(c => c.Id).HasName(idName);
            typeBuilder.Property(c => c.Id).HasColumnName(idName).ValueGeneratedOnAdd();
            // Configuration longueur des nvarchar 
            typeBuilder.Property(c => c.Name).HasMaxLength(Rules.DEFAULT_NAME_MAX_LENGHT);
            
            #region Configuration relations
            //typeBuilder.HasMany(c => c.Breweries).WithOne(br => br.Country);
            //typeBuilder.Navigation(c => c.Breweries).AutoInclude();
            #endregion
        }

        private void OnIngredientCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<IngredientEntity> typeBuilder = modelBuilder.Entity<IngredientEntity>();
            var idName = "IngredientId";
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("Ingredient").HasKey(i => i.Id).HasName(idName);
            typeBuilder.Property(i => i.Id).HasColumnName(idName).ValueGeneratedOnAdd();
            // Configuration longueur des nvarchar 
            typeBuilder.Property(i => i.Name).HasMaxLength(Rules.DEFAULT_NAME_MAX_LENGHT);
            typeBuilder.Property(i => i.Description).HasMaxLength(Rules.DEFAULT_DESCRIPTION_MAX_LENGTH);
           
            #region Configuration relations
            // BeerIngredient
            typeBuilder.HasDiscriminator<string>("Type")
                .HasValue<HopEntity>("Hop")
                .HasValue<CerealEntity>("Cereal")
                .HasValue<AdditiveEntity>("Additive");
            typeBuilder.Property("Type").HasMaxLength(Rules.INGREDIENT_TYPE_MAX_LENGTH);
            #endregion
        }

        private void OnHopCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<HopEntity> typeBuilder = modelBuilder.Entity<HopEntity>();
            // TODO validation sur AlphaAcid -> pour l'instant avec Data Annotation
        }

        private void OnCerealCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<CerealEntity> typeBuilder = modelBuilder.Entity<CerealEntity>();
            // TODO validation sur Ebc -> pour l'instant avec Data Annotation
        }

        private void OnAdditiveCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<AdditiveEntity> typeBuilder = modelBuilder.Entity<AdditiveEntity>();
            typeBuilder.Property(a => a.Use).HasMaxLength(Rules.DEFAULT_DESCRIPTION_MAX_LENGTH);
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
