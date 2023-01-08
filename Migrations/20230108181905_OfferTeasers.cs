using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKFotografiaBackend.Migrations
{
    public partial class OfferTeasers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeaserDesktop",
                table: "Offers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TeaserMobile",
                table: "Offers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeaserDesktop",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "TeaserMobile",
                table: "Offers");
        }
    }
}
