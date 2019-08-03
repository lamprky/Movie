using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Database;

namespace WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityType>().HasData(
                new EntityType { ID = Guid.Parse("4B191233-7411-48BE-851D-FA5E1C891DDB"), Name = "Movie" },
                new EntityType { ID = Guid.Parse("FC9945F4-D513-4818-AD87-B854BCADE8E6"), Name = "Genre" },
                new EntityType { ID = Guid.Parse("9D0BD902-C389-4A3E-BFB1-FC43FFDD0DEE"), Name = "Contributor" },
                new EntityType { ID = Guid.Parse("C242262F-611C-48AF-99CB-9B93496F8687"), Name = "ContributorType" }
            );

            modelBuilder.Entity<Language>().HasData(
                new Language { ID = Guid.Parse("D4423635-10ED-416E-81EE-D5D7E6746090"), Name = "English", Code = "en" },
                new Language { ID = Guid.Parse("E621BA86-B5CF-4AAE-B867-56969E71851A"), Name = "Greek", Code = "el" },
                new Language { ID = Guid.Parse("3828E370-B755-47E8-8CCC-12EBCB1CEBA4"), Name = "Italian", Code = "it" },
                new Language { ID = Guid.Parse("B6F6270D-F435-4BD4-8723-94B7659B0505"), Name = "Spanish", Code = "es" }
            );

            modelBuilder.Entity<MovieGenres>().HasKey(sc => new { sc.MovieID, sc.GenreID });
            modelBuilder.Entity<MovieContributors>().HasKey(sc => new { sc.MovieID, sc.ContributorID });
            modelBuilder.Entity<ContributorContributorTypes>().HasKey(sc => new { sc.ContributorID, sc.ContributorTypeID });

            #region ContributionTypes

            modelBuilder.Entity<ContributorType>()
            .HasMany(x => x.Translations)
            .WithOne(x => x.ContributorType)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ContributorType>()
                .HasMany(x => x.ContributorContributorTypes)
                .WithOne(x => x.ContributorType)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Contributors

            modelBuilder.Entity<Contributor>()
                .HasMany(x => x.Translations)
                .WithOne(x => x.Contributor)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contributor>()
                .HasMany(x => x.ContributorContributorTypes)
                .WithOne(x => x.Contributor)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contributor>()
                .HasMany(x => x.MovieContributors)
                .WithOne(x => x.Contributor)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Genres

            modelBuilder.Entity<Genre>()
                .HasMany(x => x.Translations)
                .WithOne(x => x.Genre)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Genre>()
                .HasMany(x => x.MovieGenres)
                .WithOne(x => x.Genre)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Movies

            modelBuilder.Entity<Movie>()
                .HasMany(x => x.Translations)
                .WithOne(x => x.Movie)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>()
                .HasMany(x => x.MovieContributors)
                .WithOne(x => x.Movie)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>()
                .HasMany(x => x.MovieGenres)
                .WithOne(x => x.Movie)
                .OnDelete(DeleteBehavior.Cascade); 

            #endregion

        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Contributor> Contributor { get; set; }
        public DbSet<ContributorType> ContributorsType { get; set; }
        public DbSet<EntityType> EntityType { get; set; }
    }
}
