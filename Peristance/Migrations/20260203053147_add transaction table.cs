using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addtransactiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance_Customer_CustomerId",
                table: "Balance");

            migrationBuilder.DropForeignKey(
                name: "FK_Balance_Organizations_OrganizationId",
                table: "Balance");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLoge_Balance_BalanceId",
                table: "PaymentLoge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Balance",
                table: "Balance");

            migrationBuilder.RenameTable(
                name: "Balance",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Balance_OrganizationId",
                table: "Transactions",
                newName: "IX_Transactions_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Balance_CustomerId",
                table: "Transactions",
                newName: "IX_Transactions_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLoge_Transactions_BalanceId",
                table: "PaymentLoge",
                column: "BalanceId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Customer_CustomerId",
                table: "Transactions",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Organizations_OrganizationId",
                table: "Transactions",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLoge_Transactions_BalanceId",
                table: "PaymentLoge");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Customer_CustomerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Organizations_OrganizationId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Balance");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OrganizationId",
                table: "Balance",
                newName: "IX_Balance_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CustomerId",
                table: "Balance",
                newName: "IX_Balance_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Balance",
                table: "Balance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_Customer_CustomerId",
                table: "Balance",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_Organizations_OrganizationId",
                table: "Balance",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLoge_Balance_BalanceId",
                table: "PaymentLoge",
                column: "BalanceId",
                principalTable: "Balance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
