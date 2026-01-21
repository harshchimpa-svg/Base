using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ServiceVendor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Services",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_VendorId",
                table: "Services",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Vendors_VendorId",
                table: "Services",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Vendors_VendorId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_VendorId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "Services",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
