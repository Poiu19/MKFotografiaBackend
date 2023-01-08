using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKFotografiaBackend.Migrations
{
    public partial class FullOfferTeaser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullOfferTeaser",
                table: "Offers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullOfferTeaser",
                table: "Offers");
        }
    }
}
