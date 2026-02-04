using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CustomersId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance_AspNetUsers_UserId",
                table: "Balance");

            migrationBuilder.DropIndex(
                name: "IX_Balance_UserId",
                table: "Balance");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Balance");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Balance",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balance_CustomerId",
                table: "Balance",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_Customer_CustomerId",
                table: "Balance",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance_Customer_CustomerId",
                table: "Balance");

            migrationBuilder.DropIndex(
                name: "IX_Balance_CustomerId",
                table: "Balance");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Balance");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Balance",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balance_UserId",
                table: "Balance",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_AspNetUsers_UserId",
                table: "Balance",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
