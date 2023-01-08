using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKFotografiaBackend.Migrations
{
    public partial class SliderPhotoOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "SliderPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "SliderPhotos");
        }
    }
}
