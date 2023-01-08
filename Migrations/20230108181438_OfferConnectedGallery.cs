using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKFotografiaBackend.Migrations
{
    public partial class OfferConnectedGallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Offers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ConnectedGalleryId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelativeURL",
                table: "Offers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ConnectedGalleryId",
                table: "Offers",
                column: "ConnectedGalleryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Galleries_ConnectedGalleryId",
                table: "Offers",
                column: "ConnectedGalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Galleries_ConnectedGalleryId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ConnectedGalleryId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ConnectedGalleryId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RelativeURL",
                table: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Offers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
