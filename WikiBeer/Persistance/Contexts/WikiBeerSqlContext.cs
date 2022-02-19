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
/// Pour des précisions sur les includes (dans les requêtes) et AutoInclude (dans le modelBuilder)
/// voir : https://docs.microsoft.com/fr-fr/ef/core/querying/related-data/eager
/// Sur les potentiel bug d'automapper concernant les références circulaire
/// https://docs.microsoft.com/fr-fr/ef/core/querying/related-data/serialization
/// Pour implémenter la barre de recherche voir les query filter 
/// https://docs.microsoft.com/en-us/ef/core/querying/filters
/// Sur la différence entre SetNull et SetClientNull (très important)
/// https://www.tektutorialshub.com/entity-framework-core/cascade-delete-in-entity-framework-core/
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
        public DbSet<HopEntity> Hops { get; set; }
        public DbSet<CerealEntity> Cereal { get; set; }
        public DbSet<AdditiveEntity> Additive { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserBeer> UserBeers { get; set; }
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

            // Entities de base (Beer)
            OnBeerCreating(modelBuilder);
            OnBreweryCreating(modelBuilder);
            OnStyleCreating(modelBuilder);
            OnColorCreating(modelBuilder);
            OnCountryCreating(modelBuilder);

            // Enities Abstracts et dérivées (Beer)           
            OnIngredientCreating(modelBuilder);
            OnHopCreating(modelBuilder);
            OnCerealCreating(modelBuilder);
            OnAdditiveCreating(modelBuilder);

            // Entities de base (User)
            OnUserCreating(modelBuilder);

            // Entities de base (Interractions User-Beer)
            OnUserBeerCreating(modelBuilder);
        }

        private void OnUserBeerCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<UserBeer> typeBuilder = modelBuilder.Entity<UserBeer>();
            var userIdName = "UserId";
            var beerIdName = "BeerId";
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("UserBeer").HasKey(ub => new { ub.UserId, ub.BeerId });
            typeBuilder.Property(ub => ub.UserId).HasColumnName(userIdName);
            typeBuilder.Property(ub => ub.BeerId).HasColumnName(beerIdName);

            #region Configuration relations
            typeBuilder.HasOne(ub => ub.User).WithMany(u => u.UserBeers).HasForeignKey(u => u.UserId);
            typeBuilder.HasOne(ub => ub.Beer).WithMany().HasForeignKey(u => u.BeerId);
            // FavoritesBeer
            //typeBuilder.HasMany(u => u.FavoriteBeers).WithMany(b => b.Users)
            //    .UsingEntity(ub => ub.ToTable("UserFavoriteBeer"));
            //typeBuilder.Navigation(u => u.FavoriteBeers).AutoInclude();
            #endregion
        }

        private void OnUserCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<UserEntity> typeBuilder = modelBuilder.Entity<UserEntity>();
            var idName = "UserId";
            // Configuration nom de table et clef primaire
            typeBuilder.ToTable("User").HasKey(u => u.Id).HasName(idName);
            typeBuilder.Property(u => u.Id).HasColumnName(idName).ValueGeneratedOnAdd();
            // Configuration longueur des nvarchar 
            typeBuilder.Property(u => u.NickName).HasMaxLength(Rules.DEFAULT_NICKNAME_MAX_LENGTH);
            typeBuilder.Property(u => u.Email).HasMaxLength(Rules.DEFAULT_MAIL_MAX_LENGTH);

            #region Configuration relations
            // Country 
            typeBuilder.HasOne(c => c.Country).WithMany();
            typeBuilder.Navigation(u => u.Country).AutoInclude();
            // UserBeer
            typeBuilder.Navigation(u => u.UserBeers).AutoInclude();
            // FavoritesBeer
            //typeBuilder.HasMany(u => u.FavoriteBeers).WithMany(b => b.Users)
            //    .UsingEntity(ub => ub.ToTable("UserFavoriteBeer"));
            //typeBuilder.Navigation(u => u.FavoriteBeers).AutoInclude();
            #endregion
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
            typeBuilder.HasOne(be => be.Brewery).WithMany(br => br.Beers).OnDelete(DeleteBehavior.SetNull);
            typeBuilder.Navigation(be => be.Brewery).AutoInclude();
            // Style
            typeBuilder.HasOne(be => be.Style).WithMany(s => s.Beers).OnDelete(DeleteBehavior.SetNull);
            typeBuilder.Navigation(be => be.Style).AutoInclude();
            // Color
            typeBuilder.HasOne(be => be.Color).WithMany(c => c.Beers).OnDelete(DeleteBehavior.SetNull);
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
            // Beer
            //typeBuilder.HasMany(br => br.Beers).WithOne();
            //typeBuilder.Navigation(br => br.Beers).AutoInclude();
            // Country 
            typeBuilder.HasOne(br => br.Country).WithMany(c => c.Breweries).OnDelete(DeleteBehavior.SetNull);
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
            //typeBuilder.HasMany(s => s.Beers).WithOne(b => b.Style);
            //typeBuilder.Navigation(s => s.Beers).AutoInclude();            
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
            //typeBuilder.HasMany<BeerEntity>().WithOne();
            //typeBuilder.Navigation(c => c.Beers).AutoInclude();            
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
            //typeBuilder.HasMany(c => c.Breweries).WithOne();
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
            ChangeTracker.LazyLoadingEnabled = false; // pour les anciennes versions d'EF (5 et moins)            
            return base.Set<TEntity>();
        }
        
    }
}
