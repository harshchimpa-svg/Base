using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Catgory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "UserProfile",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "UserAddress",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "OTPs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_OrganizationId",
                table: "UserProfile",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_OrganizationId",
                table: "UserAddress",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OTPs_OrganizationId",
                table: "OTPs",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OTPs_Organizations_OrganizationId",
                table: "OTPs",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_Organizations_OrganizationId",
                table: "UserAddress",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Organizations_OrganizationId",
                table: "UserProfile",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTPs_Organizations_OrganizationId",
                table: "OTPs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_Organizations_OrganizationId",
                table: "UserAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Organizations_OrganizationId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_OrganizationId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_OrganizationId",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_OTPs_OrganizationId",
                table: "OTPs");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "OTPs");
        }
    }
}
