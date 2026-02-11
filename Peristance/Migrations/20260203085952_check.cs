using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLoge_Transactions_BalanceId",
                table: "PaymentLoge");

            migrationBuilder.RenameColumn(
                name: "BalanceType",
                table: "Transactions",
                newName: "TransactionType");

            migrationBuilder.RenameColumn(
                name: "BalanceType",
                table: "PaymentLoge",
                newName: "TransactionType");

            migrationBuilder.RenameColumn(
                name: "BalanceId",
                table: "PaymentLoge",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLoge_BalanceId",
                table: "PaymentLoge",
                newName: "IX_PaymentLoge_TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLoge_Transactions_TransactionId",
                table: "PaymentLoge",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLoge_Transactions_TransactionId",
                table: "PaymentLoge");

            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "Transactions",
                newName: "BalanceType");

            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "PaymentLoge",
                newName: "BalanceType");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "PaymentLoge",
                newName: "BalanceId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLoge_TransactionId",
                table: "PaymentLoge",
                newName: "IX_PaymentLoge_BalanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLoge_Transactions_BalanceId",
                table: "PaymentLoge",
                column: "BalanceId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
