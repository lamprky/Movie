using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contributor",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContributorType",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EntityType",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Code = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContributorContributorTypes",
                columns: table => new
                {
                    ContributorID = table.Column<Guid>(nullable: false),
                    ContributorTypeID = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorContributorTypes", x => new { x.ContributorID, x.ContributorTypeID });
                    table.UniqueConstraint("AK_ContributorContributorTypes_ID", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContributorContributorTypes_Contributor_ContributorID",
                        column: x => x.ContributorID,
                        principalTable: "Contributor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContributorContributorTypes_ContributorType_ContributorTypeID",
                        column: x => x.ContributorTypeID,
                        principalTable: "ContributorType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieContributors",
                columns: table => new
                {
                    MovieID = table.Column<Guid>(nullable: false),
                    ContributorID = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieContributors", x => new { x.MovieID, x.ContributorID });
                    table.UniqueConstraint("AK_MovieContributors_ID", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MovieContributors_Contributor_ContributorID",
                        column: x => x.ContributorID,
                        principalTable: "Contributor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieContributors_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieID = table.Column<Guid>(nullable: false),
                    GenreID = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieID, x.GenreID });
                    table.UniqueConstraint("AK_MovieGenres_ID", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genre_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genre",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    LanguageId = table.Column<Guid>(nullable: false),
                    ContributorTypeID = table.Column<Guid>(nullable: true),
                    ContributorID = table.Column<Guid>(nullable: true),
                    GenreID = table.Column<Guid>(nullable: true),
                    MovieID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Translation_Contributor_ContributorID",
                        column: x => x.ContributorID,
                        principalTable: "Contributor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Translation_ContributorType_ContributorTypeID",
                        column: x => x.ContributorTypeID,
                        principalTable: "ContributorType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Translation_Genre_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genre",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Translation_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Translation_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("4b191233-7411-48be-851d-fa5e1c891ddb"), "Movie" },
                    { new Guid("fc9945f4-d513-4818-ad87-b854bcade8e6"), "Genre" },
                    { new Guid("9d0bd902-c389-4a3e-bfb1-fc43ffdd0dee"), "Contributor" },
                    { new Guid("c242262f-611c-48af-99cb-9b93496f8687"), "ContributorType" }
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "ID", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("d4423635-10ed-416e-81ee-d5d7e6746090"), "en", "English" },
                    { new Guid("e621ba86-b5cf-4aae-b867-56969e71851a"), "el", "Greek" },
                    { new Guid("3828e370-b755-47e8-8ccc-12ebcb1ceba4"), "it", "Italian" },
                    { new Guid("b6f6270d-f435-4bd4-8723-94b7659b0505"), "es", "Spanish" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContributorContributorTypes_ContributorTypeID",
                table: "ContributorContributorTypes",
                column: "ContributorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieContributors_ContributorID",
                table: "MovieContributors",
                column: "ContributorID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreID",
                table: "MovieGenres",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_ContributorID",
                table: "Translation",
                column: "ContributorID");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_ContributorTypeID",
                table: "Translation",
                column: "ContributorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_GenreID",
                table: "Translation",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_LanguageId",
                table: "Translation",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_MovieID",
                table: "Translation",
                column: "MovieID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContributorContributorTypes");

            migrationBuilder.DropTable(
                name: "EntityType");

            migrationBuilder.DropTable(
                name: "MovieContributors");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "Contributor");

            migrationBuilder.DropTable(
                name: "ContributorType");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
