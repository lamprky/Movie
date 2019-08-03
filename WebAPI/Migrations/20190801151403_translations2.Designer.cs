﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Data;

namespace WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190801151403_translations2")]
    partial class translations2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Models.Database.Contributor", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("Contributor");
                });

            modelBuilder.Entity("WebAPI.Models.Database.ContributorContributorTypes", b =>
                {
                    b.Property<Guid>("ContributorID");

                    b.Property<Guid>("ContributorTypeID");

                    b.Property<Guid>("ID");

                    b.HasKey("ContributorID", "ContributorTypeID");

                    b.HasAlternateKey("ID");

                    b.HasIndex("ContributorTypeID");

                    b.ToTable("ContributorContributorTypes");
                });

            modelBuilder.Entity("WebAPI.Models.Database.ContributorType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("ContributorType");
                });

            modelBuilder.Entity("WebAPI.Models.Database.EntityType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("EntityType");

                    b.HasData(
                        new
                        {
                            ID = new Guid("4b191233-7411-48be-851d-fa5e1c891ddb"),
                            Name = "Movie"
                        },
                        new
                        {
                            ID = new Guid("fc9945f4-d513-4818-ad87-b854bcade8e6"),
                            Name = "Genre"
                        },
                        new
                        {
                            ID = new Guid("9d0bd902-c389-4a3e-bfb1-fc43ffdd0dee"),
                            Name = "Contributor"
                        },
                        new
                        {
                            ID = new Guid("c242262f-611c-48af-99cb-9b93496f8687"),
                            Name = "ContributorType"
                        });
                });

            modelBuilder.Entity("WebAPI.Models.Database.Genre", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("WebAPI.Models.Database.Language", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.HasKey("ID");

                    b.ToTable("Language");

                    b.HasData(
                        new
                        {
                            ID = new Guid("d4423635-10ed-416e-81ee-d5d7e6746090"),
                            Code = "en",
                            Name = "English"
                        },
                        new
                        {
                            ID = new Guid("e621ba86-b5cf-4aae-b867-56969e71851a"),
                            Code = "el",
                            Name = "Greek"
                        },
                        new
                        {
                            ID = new Guid("3828e370-b755-47e8-8ccc-12ebcb1ceba4"),
                            Code = "it",
                            Name = "Italian"
                        },
                        new
                        {
                            ID = new Guid("b6f6270d-f435-4bd4-8723-94b7659b0505"),
                            Code = "es",
                            Name = "Spanish"
                        });
                });

            modelBuilder.Entity("WebAPI.Models.Database.Movie", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("WebAPI.Models.Database.MovieContributors", b =>
                {
                    b.Property<Guid>("MovieID");

                    b.Property<Guid>("ContributorID");

                    b.Property<Guid>("ID");

                    b.HasKey("MovieID", "ContributorID");

                    b.HasAlternateKey("ID");

                    b.HasIndex("ContributorID");

                    b.ToTable("MovieContributors");
                });

            modelBuilder.Entity("WebAPI.Models.Database.MovieGenres", b =>
                {
                    b.Property<Guid>("MovieID");

                    b.Property<Guid>("GenreID");

                    b.Property<Guid>("ID");

                    b.HasKey("MovieID", "GenreID");

                    b.HasAlternateKey("ID");

                    b.HasIndex("GenreID");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("WebAPI.Models.Database.Translation", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ContributorID");

                    b.Property<Guid?>("ContributorTypeID");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<Guid?>("GenreID");

                    b.Property<Guid>("LanguageId");

                    b.Property<Guid?>("MovieID");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("ContributorID");

                    b.HasIndex("ContributorTypeID");

                    b.HasIndex("GenreID");

                    b.HasIndex("LanguageId");

                    b.HasIndex("MovieID");

                    b.ToTable("Translation");
                });

            modelBuilder.Entity("WebAPI.Models.Database.ContributorContributorTypes", b =>
                {
                    b.HasOne("WebAPI.Models.Database.Contributor", "Contributor")
                        .WithMany("ContributorContributorTypes")
                        .HasForeignKey("ContributorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.Database.ContributorType", "ContributorType")
                        .WithMany("ContributorContributorTypes")
                        .HasForeignKey("ContributorTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Database.MovieContributors", b =>
                {
                    b.HasOne("WebAPI.Models.Database.Contributor", "Contributor")
                        .WithMany("MovieContributors")
                        .HasForeignKey("ContributorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.Database.Movie", "Movie")
                        .WithMany("MovieContributors")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Database.MovieGenres", b =>
                {
                    b.HasOne("WebAPI.Models.Database.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.Database.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Database.Translation", b =>
                {
                    b.HasOne("WebAPI.Models.Database.Contributor", "Contributor")
                        .WithMany("Translations")
                        .HasForeignKey("ContributorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.Database.ContributorType", "ContributorType")
                        .WithMany("Translations")
                        .HasForeignKey("ContributorTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.Database.Genre", "Genre")
                        .WithMany("Translations")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.Database.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.Database.Movie", "Movie")
                        .WithMany("Translations")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
