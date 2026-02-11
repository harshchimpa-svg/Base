using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changepaymentlogetopaymentlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categori_Categori_ParentId",
                table: "Categori");

            migrationBuilder.DropForeignKey(
                name: "FK_Categori_Organizations_OrganizationId",
                table: "Categori");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLoge_AspNetUsers_UserId",
                table: "PaymentLoge");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLoge_Customer_CustomerId",
                table: "PaymentLoge");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLoge_Organizations_OrganizationId",
                table: "PaymentLoge");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLoge_Transactions_TransactionId",
                table: "PaymentLoge");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Categori_CatgoryId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentLoge",
                table: "PaymentLoge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categori",
                table: "Categori");

            migrationBuilder.RenameTable(
                name: "PaymentLoge",
                newName: "PaymentLogs");

            migrationBuilder.RenameTable(
                name: "Categori",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLoge_UserId",
                table: "PaymentLogs",
                newName: "IX_PaymentLogs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLoge_TransactionId",
                table: "PaymentLogs",
                newName: "IX_PaymentLogs_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLoge_OrganizationId",
                table: "PaymentLogs",
                newName: "IX_PaymentLogs_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLoge_CustomerId",
                table: "PaymentLogs",
                newName: "IX_PaymentLogs_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Categori_ParentId",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Categori_OrganizationId",
                table: "Categories",
                newName: "IX_Categories_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentLogs",
                table: "PaymentLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Organizations_OrganizationId",
                table: "Categories",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLogs_AspNetUsers_UserId",
                table: "PaymentLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLogs_Customer_CustomerId",
                table: "PaymentLogs",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLogs_Organizations_OrganizationId",
                table: "PaymentLogs",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLogs_Transactions_TransactionId",
                table: "PaymentLogs",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Categories_CatgoryId",
                table: "Services",
                column: "CatgoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Organizations_OrganizationId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLogs_AspNetUsers_UserId",
                table: "PaymentLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLogs_Customer_CustomerId",
                table: "PaymentLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLogs_Organizations_OrganizationId",
                table: "PaymentLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLogs_Transactions_TransactionId",
                table: "PaymentLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Categories_CatgoryId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentLogs",
                table: "PaymentLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "PaymentLogs",
                newName: "PaymentLoge");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categori");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLogs_UserId",
                table: "PaymentLoge",
                newName: "IX_PaymentLoge_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLogs_TransactionId",
                table: "PaymentLoge",
                newName: "IX_PaymentLoge_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLogs_OrganizationId",
                table: "PaymentLoge",
                newName: "IX_PaymentLoge_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLogs_CustomerId",
                table: "PaymentLoge",
                newName: "IX_PaymentLoge_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "Categori",
                newName: "IX_Categori_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_OrganizationId",
                table: "Categori",
                newName: "IX_Categori_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentLoge",
                table: "PaymentLoge",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categori",
                table: "Categori",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categori_Categori_ParentId",
                table: "Categori",
                column: "ParentId",
                principalTable: "Categori",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categori_Organizations_OrganizationId",
                table: "Categori",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLoge_AspNetUsers_UserId",
                table: "PaymentLoge",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLoge_Customer_CustomerId",
                table: "PaymentLoge",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLoge_Organizations_OrganizationId",
                table: "PaymentLoge",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLoge_Transactions_TransactionId",
                table: "PaymentLoge",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Categori_CatgoryId",
                table: "Services",
                column: "CatgoryId",
                principalTable: "Categori",
                principalColumn: "Id");
        }
    }
}
