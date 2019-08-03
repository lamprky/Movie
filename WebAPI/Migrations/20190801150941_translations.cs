using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class translations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translation_ContributorType_ContributorTypeID",
                table: "Translation");

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_ContributorType_ContributorTypeID",
                table: "Translation",
                column: "ContributorTypeID",
                principalTable: "ContributorType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translation_ContributorType_ContributorTypeID",
                table: "Translation");

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_ContributorType_ContributorTypeID",
                table: "Translation",
                column: "ContributorTypeID",
                principalTable: "ContributorType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
