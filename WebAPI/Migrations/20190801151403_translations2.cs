using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class translations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Contributor_ContributorID",
                table: "Translation");

            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Genre_GenreID",
                table: "Translation");

            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Movie_MovieID",
                table: "Translation");

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Contributor_ContributorID",
                table: "Translation",
                column: "ContributorID",
                principalTable: "Contributor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Genre_GenreID",
                table: "Translation",
                column: "GenreID",
                principalTable: "Genre",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Movie_MovieID",
                table: "Translation",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Contributor_ContributorID",
                table: "Translation");

            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Genre_GenreID",
                table: "Translation");

            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Movie_MovieID",
                table: "Translation");

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Contributor_ContributorID",
                table: "Translation",
                column: "ContributorID",
                principalTable: "Contributor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Genre_GenreID",
                table: "Translation",
                column: "GenreID",
                principalTable: "Genre",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Movie_MovieID",
                table: "Translation",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
